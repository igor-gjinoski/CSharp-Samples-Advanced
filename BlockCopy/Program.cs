
namespace BlockCopy
{
    using System;
    using System.Text;

    class Program
    {
        /// <summary>
        /// BlockCopy:
        /// Copies a specified number of bytes from a source array starting at a particular offset 
        /// to a destination array starting at a particular offset.
        /// </summary>
        /// <param name="src">The source buffer.</param>
        /// <param name="srcOffset">The zero-based byte offset into src.</param>
        /// <param name="dst">The destination buffer.</param>
        /// <param name="dstOffset">The zero-based byte offset into dst.</param>
        /// <param name="count">The number of bytes to copy.</param>
        static void Main()
        {
            char[] sourceArray = new char[] { 'A', 'B', 'C', 'D' };
            char[] destinationArray = new char[5];
            int sourceArrayBytesLength = Encoding.Unicode.GetBytes(sourceArray).Length;

            Buffer.BlockCopy(sourceArray, 0, destinationArray, 0, sourceArrayBytesLength);

            Display(destinationArray);
        }

        static void Display<T>(T[] array) => Console.WriteLine(string.Join(", ", array));
    }
}
