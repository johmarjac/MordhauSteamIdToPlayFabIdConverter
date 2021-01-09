using Microsoft.Win32;
using MordhauTools.Command;
using MordhauTools.Core;
using MordhauTools.Shared.Interfaces;
using MordhauTools.Shared.Model.PlayFab.Request;
using MordhauTools.Shared.Model.PlayFab.Response;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace MordhauTools.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private bool isBusy;
        public bool IsReady
        {
            get => isBusy;
            set
            {
                isBusy = value;
                NotifyPropertyChanged();
            }
        }

        private string inputFile;
        public string InputFile
        {
            get => inputFile;
            set
            {
                inputFile = value;
                NotifyPropertyChanged();
            }
        }

        private string outputFile;
        public string OutputFile
        {
            get => outputFile;
            set
            {
                outputFile = value;
                NotifyPropertyChanged();
            }
        }

        private IInputConversionProvider selectedInputProvider;
        public IInputConversionProvider SelectedInputProvider
        {
            get => selectedInputProvider;
            set
            {
                selectedInputProvider = value;
                NotifyPropertyChanged();
            }
        }

        private IOutputConversionProvider selectedOutputProvider;
        public IOutputConversionProvider SelectedOutputProvider
        {
            get => selectedOutputProvider;
            set
            {
                selectedOutputProvider = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<IInputConversionProvider> InputProviders { get; }

        public ObservableCollection<IOutputConversionProvider> OutputProviders { get; }

        public ICommand BrowseInput { get; }

        public ICommand BrowseOutput { get; }

        public ICommand StartConversion { get; }

        private PluginLoadContext PluginLoadContext { get; }

        public MainViewModel()
        {
            PluginLoadContext = new PluginLoadContext();
            IsReady = true;
            InputProviders = new ObservableCollection<IInputConversionProvider>();
            OutputProviders = new ObservableCollection<IOutputConversionProvider>();
            BrowseInput = new RelayCommand(BrowseInput_Click);
            BrowseOutput = new RelayCommand(BrowseOutput_Click);
            StartConversion = new RelayCommand(StartConversion_Click);

            LoadPlugins();
        }

        private void LoadPlugins()
        {
            var pluginPath = Path.Combine(Environment.CurrentDirectory, "Plugins");
            if (!Directory.Exists(pluginPath))
                Directory.CreateDirectory(pluginPath);

            var plugins = Directory.GetFiles(pluginPath, "*.dll", SearchOption.AllDirectories);

            
            // Load plugins and register their providers
            InputProviders.Clear();
            OutputProviders.Clear();

            // Register providers inside the main executable
            RegisterConversionProviders(Assembly.GetExecutingAssembly());

            foreach (var plugin in plugins)
            {
                RegisterConversionProviders(PluginLoadContext.LoadFromAssemblyPath(plugin));
            }
        }

        private void RegisterConversionProviders(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsInterface)
                    continue;

                if (typeof(IInputConversionProvider).IsAssignableFrom(type))
                {
                    IInputConversionProvider inputProvider = Activator.CreateInstance(type) as IInputConversionProvider;

                    if (inputProvider != null)
                        InputProviders.Add(inputProvider);
                }

                if (typeof(IOutputConversionProvider).IsAssignableFrom(type))
                {
                    IOutputConversionProvider outputProvider = Activator.CreateInstance(type) as IOutputConversionProvider;

                    if (outputProvider != null)
                        OutputProviders.Add(outputProvider);
                }
            }
        }

        private void BrowseInput_Click()
        {
            var ofd = new OpenFileDialog();

            ofd.Multiselect = false;
            ofd.Filter = "All Files|*.*";

            if (ofd.ShowDialog().Value)
            {
                InputFile = ofd.FileName;
            }
        }

        private void BrowseOutput_Click()
        {
            var sfd = new SaveFileDialog();

            sfd.Filter = "All Files|*.*";

            if (sfd.ShowDialog().Value)
            {
                OutputFile = sfd.FileName;
            }
        }

        private async void StartConversion_Click()
        {
            IsReady = false;

            if (SelectedInputProvider == null || SelectedOutputProvider == null)
            {
                MessageBox.Show("You need to select Input- and Output Provider first.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                IsReady = true;
                return;
            }

            var loginReqObj = new LoginWithCustomIDRequest
            {
                TitleId = "12D56",
                CustomId = $"SteamPlayFabConverter",
                CreateAccount = false
            };

            var loginResponse = await PlayFabApiHelper.LoginWithCustomID(loginReqObj);

            if (loginResponse is ApiErrorWrapper loginErrorResult)
            {
                MessageBox.Show($"Login failed with the following error: {loginErrorResult.ErrorMessage}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                IsReady = true;
            }

            if (loginResponse is LoginWithCustomIDResponse loginResult)
            {
                var convertReqObj = new GetPlayFabIDsFromSteamIDsRequest();

                var pluginData = await SelectedInputProvider.ImportData(InputFile);
                if(pluginData == null)
                {
                    MessageBox.Show($"{SelectedInputProvider.ProviderName} returned NULL!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    IsReady = true;
                    return;
                }

                convertReqObj.SteamStringIDs.AddRange(pluginData);

                var convertResponse = await PlayFabApiHelper.GetPlayFabIDsFromSteamIDs(loginResult.Data.SessionTicket, "12D56", convertReqObj);

                if (convertResponse is ApiErrorWrapper convertErrorResult)
                {
                    MessageBox.Show($"Conversion failed with the following error: {convertErrorResult.ErrorMessage}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    IsReady = true;
                }

                if (convertResponse is GetPlayFabIDsFromSteamIDsResponse convertResult)
                {
                    await SelectedOutputProvider.ExportData(OutputFile, convertResult.SteamPlayFabPairs.Data);

                    MessageBox.Show($"Conversion for {convertResult.SteamPlayFabPairs.Data.Count} Steam IDs was successful.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    IsReady = true;
                }
            }
            else
            {
                MessageBox.Show($"Conversion failed with the following error: Unhandled Response.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                IsReady = true;
            }
        }
    }
}
