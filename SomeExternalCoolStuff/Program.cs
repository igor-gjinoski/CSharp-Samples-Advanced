
namespace SomeExternalCoolStuff
{
    using System;
    using System.Collections.Generic;

    using MoreLinq; 
    using Humanizer; 

    class Program
    {
        static void Main()
        {
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

        public static string ToCsv<T>(this IEnumerable<T> enumerable, bool withNewLine = false)
        {
            return enumerable.ToDelimitedString(withNewLine ? CsvDelimiterWithNewLine : CsvDelimiter);
        }

        public static string ToTitleCase(this string @string)
        {
            return @string.Titleize();
        }

        public static string ToFriendlyString(this DateTime dateTime)
        {
            return dateTime.Humanize();
        }

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
