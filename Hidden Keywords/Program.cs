namespace Hidden_Keywords
{
    using System;
    using System.Text;

    unsafe class Program
    {
        /// <summary>
        /// __reftype - Used to get Type object from a TypedReference.
        /// __refvalue - It is used to fetch the value from a reference object. You can use this to get the actual object from TypedReference object.
        /// __makeref - Gives the TypedReference object from the object itself. This is just the reverse to __refvalue.
        /// </summary>
        static unsafe void Main(string[] args)
        {
            int x = 3;
            var xRef = __makeref(x);
            Console.WriteLine(__reftype(xRef)); // Prints "System.Int32"
            Console.WriteLine(__refvalue(xRef, int)); // Prints "3"

            __refvalue(xRef, int) = 10;
            Console.WriteLine(__refvalue(xRef, int)); // Prints "10"

            __arglistDemoMethod(__arglist(1, 2, x, "Str"));

            Fixed();
            Checked();
            StackAlloc();
        }


        /// <summary>
        /// Generally we used to send parameters to a method 
        /// by having a list of arguments specified in the method head. 
        /// If we want to pass a new set of arguments we need to have Method Overloading. 
        /// </summary>
        /// <param name="__arglist"></param>
        public static void __arglistDemoMethod(__arglist)
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


        /// <summary>
        /// Fixed statement sets the pointer to be in a fixed memory address so that, 
        /// it will not be moved to anywhere even if Garbage Collection Thread is invoked.
        /// </summary>
        public unsafe static void Fixed()
        {
            int[] a = new int[] { 1, 2, 3 };
            fixed (int* pt = a)
            {
                int* c = pt;
                Console.WriteLine("Value : " + *c);
            }
        }


        /// <summary>
        /// Used to control arithmetic overflow context. 
        /// Checked keyword throws OverflowException when an arithmetic operation overflows the necessary size.
        /// </summary>
        public static void Checked()
        {
            int x = int.MaxValue;
            int y = int.MaxValue;
            int z = checked(x + y);
        }


        /// <summary>
        ///  Allocates memory dynamically from stack. 
        ///  stackalloc is used to acquire memory quickly when it is very essential. 
        ///  We can use the advantage of Fast accessible memory when we use it from Stack.
        /// </summary>
        public static void StackAlloc()
        {
            Span<int> numbers = stackalloc int[1000];
        }
    }
}
