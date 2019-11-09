
namespace Method_Deprecation
{
    using System;
    using System.IO;

    public class Program
    {
        /// <summary>
        /// [Obsolete] attribute
        /// Marks a method that is about to be removed in the future versions.
        /// The attribute generates a compiler warning or compiler error.
        /// Pass message as first argument.
        /// And for second argument -
        /// false for warning and true for error
        /// </summary>
        static void Main()
        {
            string text = string.Empty;

            // text = Read("path"); /* Error */
            // text = ReadText("path"); /* Warning */
            text = ReadTextFile("path"); /* Good to go */
        }

        public static string ReadTextFile(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                /* Read the stream to a string, and return it. */
                return sr.ReadToEnd();
            }
        }

        [Obsolete("ReadText() method is deprecated. Use CreateConsoleWriter() instead.", false)] // true for error
        public static string ReadText(string filePath)
        {
            /* Some obsolete logic */
            return string.Empty;
        }

        [Obsolete("Read() method is deprecated. Use CreateConsoleWriter() instead.", true)] // false for warning
        public static string Read(string filePath)
        {
            /* Some obsolete logic */
            return string.Empty;
        }
    }
}

