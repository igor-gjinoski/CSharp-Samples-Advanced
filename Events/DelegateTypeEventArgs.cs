
namespace Events
{
    using System;

    public class DelegateTypeEventArgs : EventArgs
    {
        public DelegateTypeEventArgs(DelegateType type)
        {
            Type = type;
        }

        public DelegateType Type { get; set; }
    }
}
