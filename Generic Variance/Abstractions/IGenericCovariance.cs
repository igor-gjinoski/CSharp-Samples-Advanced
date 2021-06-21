
namespace Generic_Variance.Contracts
{
    public interface IGenericCovariance<out T>
    {
        // Invalid variance:
        // The type parameter 'T' must be contravariantly valid on 'ICovariantGeneric<T>.Method(T)'.
        // 'T' is covariant.
        //// T Method(T parameter);

        T Method();
    }

    public class GenericCovariance<T> : IGenericCovariance<T>
    {
        public T Method()
        {
            return default(T);
        }
    }
}
