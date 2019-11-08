
namespace Tuples_in_CSharp7
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    class Program
    {
        /// <summary>
        /// In C# 7 the Tuple is ValueTuple Struct
        /// Methods return a single object. 
        /// Tuples enable you to package multiple values in that single object more easily.
        /// </summary>
        static void Main(string[] args)
        {
            ICollection<int> collection = new List<int> { 1, 2, 3, 4, 5 };

            var result = GetCountAndAverage(collection);

            /* Benefits: */
            /* Lightweight syntax */
            /* Meaningful names of the values */
            Console.WriteLine($"Items count: {result.Count}\nAverage: {result.Average}");

            OtherCoolStuff();
        }

        public static (int Count, double Average) GetCountAndAverage(ICollection<int> source)
            => (source.Count, (double)source.Sum() / 2);

        // OtherCoolStuff
        // Passing values to Method by name
        public static void OtherCoolStuff()
        {
            /* We can pass values to method without following the exact argument in order */

            int intValue = 7;
            double doubleValue = 5.5;

            /* We can Pass values the expected way */
            var result = MultiplyAndDivide(intValue, doubleValue);

            /* Or in custome order */
            result = MultiplyAndDivide(SecondValue: doubleValue, FirstValue: intValue);

            Console.WriteLine($"Multiply: {result.v1}\nDivide: {result.v2:F4}");
        }

        // The method return two objects
        public static (double v1, double v2) MultiplyAndDivide(int FirstValue, double SecondValue)
            => ((FirstValue * SecondValue), (FirstValue / SecondValue));
    }
}
