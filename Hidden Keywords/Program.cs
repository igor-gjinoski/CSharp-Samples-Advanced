
namespace Hidden_Keywords
{
    using System;

    class Program
    {
        /// <summary>
        /// __reftype - Used to get Type object from a TypedReference.
        /// __refvalue - It is used to fetch the value from a reference object. You can use this to get the actual object from TypedReference object.
        /// __makeref - Gives the TypedReference object from the object itself. This is just the reverse to __refvalue.
        /// </summary>
        static void Main(string[] args)
        {
            int x = 3;
            var xRef = __makeref(x);
            Console.WriteLine(__reftype(xRef)); // Prints "System.Int32"
            Console.WriteLine(__refvalue(xRef, int)); // Prints "3"

            __refvalue(xRef, int) = 10;
            Console.WriteLine(__refvalue(xRef, int)); // Prints "10"

            Console.WriteLine(ParamLength(__arglist(1, 2, x, "Str"))); // Prints "4"
        }

        /// <summary>
        /// Generally we used to send parameters to a method 
        /// by having a list of arguments specified in the method head. 
        /// If we want to pass a new set of arguments we need to have Method Overloading. 
        /// </summary>
        /// <param name="__arglist"></param>
        /// <returns></returns>
        public static int ParamLength(__arglist)
        {
            ArgIterator iterator = new ArgIterator(__arglist);
            return iterator.GetRemainingCount();
        }
    }
}
