using System;
using System.Windows.Input;

namespace MordhauTools.Command
{
    public class RelayCommand : ICommand
    {

        private readonly Action _action;

        public RelayCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            _action?.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }
}
