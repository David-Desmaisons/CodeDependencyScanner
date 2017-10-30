using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Micro.MVVM.Async
{
    public sealed class TaskCommand<TResult, TProgress> : TaskCommandBase<TResult>
    {
        public IObservable<TProgress> Progress { get; }

        private event EventHandler<TProgress> _OnProgress;
        private Func<CancellationToken, IProgress<TProgress>, TResult> _Process;

        public TaskCommand(Func<CancellationToken, IProgress<TProgress>, TResult> process)
        {
            _Process = process;
  
            Progress = Observable.FromEventPattern<TProgress>(evt => _OnProgress += evt, evt => _OnProgress -= evt)
                    .Select(evtArg => evtArg.EventArgs);
        }

        protected override async Task<TResult> Process(CancellationToken cancellationToken)
        {
            using (var progress = new CancellableProgress<TProgress>(OnProgress, cancellationToken))
            {
                return await Task.Run(() => _Process(cancellationToken, progress), cancellationToken);
            }
        }

        private void OnProgress(TProgress progress)
        {
            _OnProgress(this, progress);
        }
    }
}
