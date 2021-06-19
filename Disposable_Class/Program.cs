
namespace Disposable_Class
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Win32.SafeHandles;

    class Program
    {
        static void Main()
        {
        }
    }

    public class Disposable : IDisposable
    {
        private readonly IntPtr _unmanagedResource;
        private readonly SafeHandle _managedResource;

        public Disposable()
        {
            _unmanagedResource = Marshal.AllocHGlobal(sizeof(int));
            _managedResource = new SafeFileHandle(new IntPtr(), true);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isManualDisposing)
        {
            ReleaseUnmanagedResource(_unmanagedResource);
            if (isManualDisposing)
                ReleaseManagedResource(_managedResource);
        }

        private void ReleaseUnmanagedResource(IntPtr intPtr)
        {
            Marshal.AllocHGlobal(intPtr);
        }

        private void ReleaseManagedResource(SafeHandle safeHandle)
        {
            if (safeHandle != null)
                safeHandle.Dispose();
        }

        ~Disposable() { Dispose(false); }
    }
}
