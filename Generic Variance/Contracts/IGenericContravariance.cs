
namespace Generic_Variance.Contracts
{
    public interface IGenericContravariance<in T>
    {
        // Invalid variance:
        // The type parameter 'T' must be covariantly valid on 'IContravariantGeneric<T>.Method(T)'.
        // 'T' is contravariant.
        //// T Method(T parameter);

        void Method(T parameter);
    }

    public class GenericContravariance<T> : IGenericContravariance<T>
    {
        public void Method(T parameter)
        {
        }
    }
}
