
namespace Runtime_sizeof_T
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(SizeOf<int>());
        }

        unsafe public static int SizeOf<T>() where T : struct
        {
            T[] tArray = new T[2];

            var tRef0 = __makeref(tArray[0]);
            var tRef1 = __makeref(tArray[1]);

            IntPtr ptrToT0 = *((IntPtr*)&tRef0);
            IntPtr ptrToT1 = *((IntPtr*)&tRef1);

            return (int)(((byte*)ptrToT1) - ((byte*)ptrToT0));
        }
    }
}

// http://benbowen.blog/post/fun_with_makeref/