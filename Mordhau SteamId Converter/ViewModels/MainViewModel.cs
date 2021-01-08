using Microsoft.Win32;
using MordhauTools.Command;
using MordhauTools.Model.PlayFab.Request;
using MordhauTools.Model.PlayFab.Response;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
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

        public ICommand BrowseInput { get; }

        public ICommand BrowseOutput { get; }

        public ICommand StartConversion { get; }

        public MainViewModel()
        {
            IsReady = true;
            BrowseInput = new RelayCommand(BrowseInput_Click);
            BrowseOutput = new RelayCommand(BrowseOutput_Click);
            StartConversion = new RelayCommand(StartConversion_Click);
        }

        private void BrowseInput_Click()
        {
            var ofd = new OpenFileDialog();

            ofd.Multiselect = false;
            ofd.Filter = "Text File|*.txt";

            if (ofd.ShowDialog().Value)
            {
                InputFile = ofd.FileName;
            }
        }

        private void BrowseOutput_Click()
        {
            var sfd = new SaveFileDialog();

            sfd.Filter = "Json File|*.json";

            if (sfd.ShowDialog().Value)
            {
                OutputFile = sfd.FileName;
            }
        }

        private async void StartConversion_Click()
        {
            IsReady = false;

            var loginReqObj = new LoginWithCustomIDRequest
            {
                TitleId = "12D56",
                CustomId = $"Steam;127.0.0.1:{Environment.TickCount}",
                CreateAccount = true
            };

            var loginResponse = await PlayFabApiHelper.LoginWithCustomID(loginReqObj);

            if(loginResponse is ApiErrorWrapper loginErrorResult)
            {
                MessageBox.Show($"Login failed with the following error: {loginErrorResult.ErrorMessage}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                IsReady = true;
            }

            if(loginResponse is LoginWithCustomIDResponse loginResult)
            {
                var convertReqObj = new GetPlayFabIDsFromSteamIDsRequest();

                // Fill with data
                foreach (var line in await File.ReadAllLinesAsync(InputFile))
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    convertReqObj.SteamStringIDs.Add(line.Trim());
                }

                var convertResponse = await PlayFabApiHelper.GetPlayFabIDsFromSteamIDs(loginResult.Data.SessionTicket, "12D56", convertReqObj);

                if (convertResponse is ApiErrorWrapper convertErrorResult)
                {
                    MessageBox.Show($"Conversion failed with the following error: {convertErrorResult.ErrorMessage}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    IsReady = true;
                }

                if(convertResponse is GetPlayFabIDsFromSteamIDsResponse convertResult)
                {
                    var encodedJson = JsonConvert.SerializeObject(convertResult.SteamPlayFabPairs.Data, Formatting.Indented);
                    File.WriteAllText(OutputFile, encodedJson);

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
