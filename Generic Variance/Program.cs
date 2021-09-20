
namespace Generic_Variance
{
    using Generic_Variance.Classes;
    using Generic_Variance.Contracts;

    class Program
    {
        static void Main(string[] args)
        {
            ContravariantGeneric();
            CovariantGeneric();
            AnotherExample();
        }

        public static void ContravariantGeneric()
        {
            IGenericContravariance<MiddleClass> genericMiddle = new GenericContravariance<MiddleClass>();
            genericMiddle.Method(new MiddleClass());

            // This will produce compile-time error:
            // Cannot implicitly convert type 'IContravariantGeneric<MiddleClass>' to 'IContravariantGeneric<BaseClass>'.
            // An explicit conversion exists (are you missing a cast?)
            //// IGenericContravariance<BaseClass> genericBase = genericMiddle;

            // This is OK here:
            IGenericContravariance<LastClass> genericLast = genericMiddle;
            genericLast.Method(new LastClass());
        }

        public static void CovariantGeneric()
        {
            IGenericCovariance<MiddleClass> genericMiddle = new GenericCovariance<MiddleClass>();
            MiddleClass result = genericMiddle.Method();

            // This is OK here:
            IGenericCovariance<BaseClass> genericBase = genericMiddle;
            BaseClass baseResult = genericBase.Method();
        }
    }


    // Another Example

    /// <summary>
    /// Combining the two interfaces (Covariant and Contravariant),
    /// we receive one Invariant interface
    /// </summary>
    public interface ISequence<T> :
        ISequenceReader<T>,
        ISequenceWriter<T>
    {
    }

    /// <summary>
    /// Covariance only applies to types
    /// that are being returned from methods
    /// in a generic interface
    /// </summary>
    public interface ISequenceReader<out T>
    {
        T Read();
    }

    /// <summary>
    /// Contravariance only applies to types
    /// that are being written or provided to methods
    /// in a generic interface,
    /// as parameters.
    /// </summary>
    public interface ISequenceWriter<in T>
    {
        void Write(T arg);
    }


    public class Sequence<T> : ISequence<T>
    {
        private T obj;

        public T Read()
        {
            return obj;
        }

        public void Write(T arg)
        {
            obj = arg;
        }
    }
}
