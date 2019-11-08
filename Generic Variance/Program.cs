
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
}
