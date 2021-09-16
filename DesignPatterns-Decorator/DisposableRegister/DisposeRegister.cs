using System;
using System.Collections.Concurrent;

namespace DesignPatterns.DisposableRegister
{
    public class DisposeRegister : IDisposeRegister
    {
        private readonly ConcurrentBag<IDisposable> _disposables;

        public DisposeRegister()
        {
            _disposables = new ConcurrentBag<IDisposable>();
        }

        public void Register(IDisposable flowComponent)
        {
            _disposables.Add(flowComponent);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var disposable in _disposables)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
