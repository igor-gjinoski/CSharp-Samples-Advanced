
namespace SomeExternalCoolStuff
{
    using System;
    using System.Collections.Generic;

    using MoreLinq; // Extra methods for LINQ - ToDelimitedString()
    using Humanizer; // Manipulating and displaying value types -Titleize()

    class Program
    {
        static void Main()
        {
            /* All of the samples are without exception handling */
            var listOfStrings = new List<string>()
            {
                "SomeRandomString",
                "another random string"
            };

            Console.WriteLine(
                listOfStrings.ToCsv() // Comma-separated collection
                + "\n" +
                listOfStrings[0].ToTitleCase()); // Formats a string as title case

            Season season = "Winter1".ConvertToEnum<Season>();
            Console.WriteLine(season);
        }
    }

    enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }

    static class Extensions
    {
        private const string CsvDelimiter = ",";
        private const string CsvDelimiterWithNewLine = ",\r\n";

        /// <summary>
        /// Presents a collection as a comma-separated string
        /// </summary>
        /// <typeparam name="T">Type of collection item</typeparam>
        /// <param name="enumerable">Collection</param>
        /// <param name="withNewLine">Whether to write a newline after each item</param>
        public static string ToCsv<T>(this IEnumerable<T> enumerable, bool withNewLine = false)
        {
            return enumerable.ToDelimitedString(withNewLine ? CsvDelimiterWithNewLine : CsvDelimiter);
        }

        /// <summary>
        /// Formats a string as title case, capitalizing major words
        /// </summary>
        /// <param name="string">Input string</param>
        public static string ToTitleCase(this string @string)
        {
            return @string.Titleize();
        }

        /// <summary>
        /// Represents a date as a human-friendly duration, e.g. "two days ago"
        /// </summary>
        /// <param name="dateTime">Date to format</param>
        public static string ToFriendlyString(this DateTime dateTime)
        {
            return dateTime.Humanize();
        }

        /// <summary>
        /// Convert string value to Enum value
        /// </summary>
        public static T ConvertToEnum<T>(this string @string)
        {
            return (T)Convert.ChangeType(@string.DehumanizeTo(typeof(T)), typeof(T));
        }

        /*
             * The old way:
             * First we need constraints for T
                => where T : struct, Enum

             * Then we must use "TryParse()" function
                => T enumValue;
                   if (Enum.TryParse(@string, true, out enumValue))
                       return enumValue;
        */
    }
}
