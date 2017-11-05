using Neutronium.MVVMComponents;
using Neutronium.MVVMComponents.Relay;
using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Micro.MVVM.Async
{
    public abstract class TaskCommandBase<TResult> : ViewModelBase
    {
        private bool _Computing;
        public bool Computing 
        {
            get { return _Computing; }
            private set
            {
                if (Set(ref _Computing, value))
                {
                    UpdateCommandStatus();
                }
            }
        }

        private bool _CanBeRun = true;
        public bool CanBeRun
        {
            get { return _CanBeRun; }
            set
            {
                _CanBeRun = value;
                UpdateCommandStatus();
            }
        }

        private readonly RelayToogleCommand _Run;
        public ICommand Run => _Run;

        public ISimpleCommand Cancel { get;  }
        
        public IObservable<CommandResult<TResult>> Results { get; }
        private event EventHandler<CommandResult<TResult>> _OnResult;
        private CancellationTokenSource _CancellationTokenSource;

        protected TaskCommandBase()
        {
            Results = Observable.FromEventPattern<CommandResult<TResult>>(evt => _OnResult += evt, evt => _OnResult -= evt) 
                                .Select(evtArg => evtArg.EventArgs);

            Cancel = new RelaySimpleCommand(DoCancel);
            _Run = new RelayToogleCommand(DoCompute);
        }

        private void UpdateCommandStatus()
        {
            _Run.ShouldExecute = !_Computing && CanBeRun;
        }

        private void DoCancel()
        {
            _CancellationTokenSource?.Cancel();
        }

        protected abstract Task<TResult> Process(CancellationToken cancellationToken);

        private async void DoCompute()
        {
            _CancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _CancellationTokenSource.Token;
            try
            {
                Computing = true;
                var result = await Process(cancellationToken);
                Computing = false;
                if (!cancellationToken.IsCancellationRequested)
                {
                    _OnResult(this, new CommandResult<TResult>(result));
                }
                else
                {
                    _OnResult(this, new CommandResult<TResult>());
                }
            }
            catch (TaskCanceledException)
            {
                _OnResult(this, new CommandResult<TResult>());
            }
            catch (Exception exception)
            {
                _OnResult(this, new CommandResult<TResult>(exception));
            }
            finally
            {
                Computing = false;
            }
        }
    }
}
