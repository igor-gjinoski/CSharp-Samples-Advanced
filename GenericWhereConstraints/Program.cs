
namespace GenericWhereConstraints
{
    class Program
    {
        static void Main()
        {
        }
    }


    class TmpClass<T, U>
        where T : class //  T is reference type
        where U : struct // U is value type
    { 
    }

    class NotNullContainer<T>
        where T : notnull // T must be value type or non-nullable type.
    {
    }
    public class GenericClass<T> 
        where T : new() // T must have accessible constructor. new() makes it possible to create an instance of a type T.
    {
    }

    class InterfaceConstraint<T> 
        where T : IContract // T must implement IContract
    {
    }

    interface IContract
    {
    }
}
