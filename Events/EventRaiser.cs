
namespace Events
{
    using System;

    class EventRaiser
    {
        public event EventHandler<DelegateTypeEventArgs> DelegateTypeSelected;
        public event EventHandler ExecutionCompleted;

        public void Start()
        {
            foreach (var type in Enum.GetValues(typeof(DelegateType)))
            {
                System.Threading.Thread.Sleep(1000);

                DelegateType enumType = (DelegateType)Enum.Parse(typeof(DelegateType), type.ToString());
                OnDelegateType(enumType);
            }
            System.Threading.Thread.Sleep(1000);
            OnCompleted();
        }

        protected virtual void OnDelegateType(DelegateType type)
        {
            DelegateTypeSelected?.Invoke(this, new DelegateTypeEventArgs(type));
        }

        protected virtual void OnCompleted()
        {
            ExecutionCompleted?.Invoke(this, EventArgs.Empty);
        }
    }

    public enum DelegateType
    {
        SingleCast,
        MultiCast,
        Generic
    }
}
