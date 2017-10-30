using System;

namespace Micro.MVVM.Async
{
    public class CommandResult<TResult>
    {
        public TResult Result { get; }
        public Exception Exception { get; }
        public bool Success => (!Cancelled && !HasError);
        public bool HasError => (Exception != null);
        public bool Cancelled { get; }

        public CommandResult()
        {
            Result = default(TResult);
            Cancelled = true;
        }

        public CommandResult(TResult result)
        {
            Result = result;
            Exception = null;
        }

        public CommandResult(Exception exception)
        {
            Result = default(TResult);
            Exception = exception;
        }
    }
}