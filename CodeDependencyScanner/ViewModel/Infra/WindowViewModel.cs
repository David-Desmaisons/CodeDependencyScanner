using System;
using System.Windows;
using Micro.MVVM;
using Neutronium.MVVMComponents;
using Neutronium.MVVMComponents.Relay;

namespace CodeDependencyScanner.ViewModel.Infra
{
    public class WindowViewModel : ViewModelBase, IWindowViewModel
    {
        private readonly Window _Window;

        public ISimpleCommand Close { get; }
        public ISimpleCommand Minimize { get; }
        public ISimpleCommand Maximize { get; }
        public ISimpleCommand Normalize { get; }
        public WindowState State
        {
            get { return _Window.WindowState; }
            set { _Window.WindowState = value; }
        }

        public WindowViewModel(Window window)
        {
            _Window = window;
            Close = new RelaySimpleCommand(() => _Window.Close());
            Minimize = new RelaySimpleCommand(() => State = WindowState.Minimized);
            Maximize = new RelaySimpleCommand(() => State = WindowState.Maximized);
            Normalize = new RelaySimpleCommand(() => State = (State == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized);

            _Window.StateChanged += StateChanged;
        }

        private void StateChanged(object sender, EventArgs e)
        {
            PropertyHasChanged(nameof(State));
        }

        public void Dispose()
        {
            _Window.StateChanged -= StateChanged;
        }
    }
}
