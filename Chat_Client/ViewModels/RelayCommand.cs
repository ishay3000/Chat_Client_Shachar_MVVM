using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chat_Client.ViewModels
{
    public class RelayCommand : ICommand
    {
        private Predicate<object> _canExecute;
        private Action _execute;

        public RelayCommand(Action execute)
        {
            //this._canExecute = canExecute;
            this._execute = execute;
        }

        public RelayCommand(Action execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            //if (_canExecute != null)
            //{
            //    return CanExecute(parameter);
            //}

            bool? fl = _canExecute?.Invoke(parameter);
            return fl ?? true;
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
