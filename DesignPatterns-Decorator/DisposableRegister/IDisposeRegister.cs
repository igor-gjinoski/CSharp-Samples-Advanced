using System;

namespace DesignPatterns.DisposableRegister
{
    public interface IDisposeRegister : IDisposable
    {
        void Register(IDisposable disposable);
    }
}
