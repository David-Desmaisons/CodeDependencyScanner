using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Micro.MVVM.Relay
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _Execute;
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

        public RelayCommand(Action<object> execute)
        {
            _Execute = execute;
        }

        public RelayCommand(Action execute)
        {
            _Execute = _ => execute();
        }

        public bool CanExecute(object parameter)
        {
            return _Canexecute;
        }

        [DebuggerStepThrough]
        public void Execute(object parameter)
        {
            _Execute(parameter);
        }
    }
}
