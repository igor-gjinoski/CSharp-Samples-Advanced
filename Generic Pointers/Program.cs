
namespace Generic_Pointers
{
    using System;
    using System.Runtime.CompilerServices;

    unsafe class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteGenericToPtr<T>(IntPtr dest, T value, int sizeOfT) where T : struct
        {
            byte* bytePtr = (byte*)dest;

            TypedReference valueref = __makeref(value);
            byte* valuePtr = (byte*)*((IntPtr*)&valueref);

            for (int i = 0; i < sizeOfT; ++i)
                bytePtr[i] = valuePtr[i];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadGenericFromPtr<T>(IntPtr source, int sizeOfT) where T : struct
        {
            byte* bytePtr = (byte*)source;

            T result = default(T);
            TypedReference resultRef = __makeref(result);
            byte* resultPtr = (byte*)*((IntPtr*)&resultRef);

            for (int i = 0; i < sizeOfT; ++i)
                resultPtr[i] = bytePtr[i];
            
            return result;
        }
    }
}

