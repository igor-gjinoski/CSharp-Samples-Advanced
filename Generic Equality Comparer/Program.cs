
namespace Generic_Equality_Comparer
{
    using System;
    using System.Collections.Generic;

    class DemoClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class Program
    {
        static void Main()
        {
        }
    }

    public class GenericCompare<T> : IEqualityComparer<T> where T : class
    {
        private Func<T, object> _exp { get; set; }

        public GenericCompare(Func<T, object> exp)
            => _exp = exp;

        public bool Equals(T x, T y)
        {
            if (x == null || y == null)
                throw new ArgumentNullException();

            return _exp.Invoke(x).Equals(_exp.Invoke(y))
                ? true
                : false;
        }

        public int GetHashCode(T obj)
            => obj != null
            ? obj.GetHashCode()
            : throw new ArgumentNullException();
    }
}
