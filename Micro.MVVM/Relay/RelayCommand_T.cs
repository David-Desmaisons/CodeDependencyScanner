using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Micro.MVVM.Relay
{
    public class RelayCommand<T>: ICommand where T: class
    {
        private readonly Action<T> _Execute;
        private bool _Canexecute = true;

        public bool Executable
        {
            get { return _Canexecute; }
            set
            {
                _Canexecute = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<T> execute)
        {
            _Execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return _Canexecute;
        }

        [DebuggerStepThrough]
        public void Execute(object parameter)
        {
            _Execute(parameter as T);
        }
    }
}
