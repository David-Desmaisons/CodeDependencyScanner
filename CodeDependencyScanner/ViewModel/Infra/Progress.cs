using System;
using System.Threading;
using System.Windows.Threading;

namespace CodeDependencyScanner.ViewModel.Infra
{
    internal class Progress<T> : IProgress<T>
    {
        private readonly Action<T> _Action;
        private readonly Dispatcher _Dispatcher;
        private readonly CancellationTokenSource _CancellationTokenSource;

        public Progress(Action<T> action, CancellationToken cancellationToken, Dispatcher dispatcher=null)
        {
            _CancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            var localCancellationToken = _CancellationTokenSource.Token;

            _Action = (message) =>
            {
                if (!localCancellationToken.IsCancellationRequested)
                {
                    action(message);
                }
            };
            _Dispatcher = dispatcher?? Dispatcher.CurrentDispatcher;
        }

        public void Stop()
        {
            _CancellationTokenSource.Cancel();
        }

        public void Report(T value)
        {
            _Dispatcher.BeginInvoke(_Action, DispatcherPriority.DataBind, value);    
        }
    }
}
