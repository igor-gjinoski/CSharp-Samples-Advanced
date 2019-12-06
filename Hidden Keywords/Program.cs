
namespace Hidden_Keywords
{
    using System;
    using System.Text;

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

            ParamLength(__arglist(1, 2, x, "Str"));
        }

        /// <summary>
        /// Generally we used to send parameters to a method 
        /// by having a list of arguments specified in the method head. 
        /// If we want to pass a new set of arguments we need to have Method Overloading. 
        /// </summary>
        /// <param name="__arglist"></param>
        public static void ParamLength(__arglist)
        {
            var arglistBuilder = new StringBuilder("\n__arglist\n");

            /* Use __arglist only with instance of ArgIterator */
            ArgIterator iterator = new ArgIterator(__arglist);

            /* Get the current N of elements in the iterator */
            /* If we get an element from the iterator the .GetRemainingCount() will show only the remaining */
            int lengthOfIterator = iterator.GetRemainingCount();
            arglistBuilder.AppendLine($"Initial values: {lengthOfIterator}");

            while (iterator.GetRemainingCount() > 0)
            {
                /* The items in __arglist are TypedReference so we need to cast them! */
                /* TypedReference.ToObject({item}) */
                var item = TypedReference.ToObject(iterator.GetNextArg());

                arglistBuilder.AppendLine($"Arg Type: {item.GetType()} --- Arg value: {item} --- Items remaining: {iterator.GetRemainingCount()}");
            }
            Console.WriteLine(arglistBuilder.ToString());
        }
    }
}
