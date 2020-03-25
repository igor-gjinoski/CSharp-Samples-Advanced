
namespace Swich_In_CSharp_8
{
    using System;

    // Helper classes
    // ------------------------------------------------------------
    enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }

    public class Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y) => (X, Y) = (x, y);

        public void Deconstruct(out int x, out int y) =>
            (x, y) = (X, Y);
    }

    public class Country
    {
        public string Code { get; }

        public Country(string code) => Code = code;
    }
    // ------------------------------------------------------------

        
    class Program
    {
        static void Main()
        {
            string summerSeason = SeasonToString(Season.Summer);
            string monthAndSeason = MonthInSeasonToString(Season.Spring, "January");
        }


        /// <summary>
        /// Switch expressions
        /// </summary>
        static string SeasonToString(Season season) 
            => season switch
            {
                Season.Spring => Season.Spring.ToString(),
                Season.Summer => Season.Summer.ToString(),
                Season.Autumn => Season.Autumn.ToString(),
                Season.Winter => Season.Winter.ToString(),

                _ => throw new ArgumentException(message: "Invalid season value", paramName: nameof(Season)),
            };


        /// <summary>
        /// Tuple patterns
        /// </summary>
        static string MonthInSeasonToString(Season season, string month)
            => (season, month) switch
            {
                (Season.Spring, "January") => $"{month} is in the {Season.Spring.ToString()}",
                (Season.Spring, "February") => $"{month} is in the {Season.Spring.ToString()}",
                // Rest ...

                (_, _) => throw new ArgumentException(
                    message: "Invalid values", 
                    paramName: string.Join(" ", new[] { nameof(Season), month })),
            };


        /// <summary>
        /// Property patterns
        /// </summary>
        public static string CountryCodeToFullName(Country code) =>
            code switch
            {
                { Code: "BG" } => "Bulgaria",
                // Rest ...

                _ => throw new ArgumentException(message: "Invalid country code"),
            };


        /// <summary>
        /// Positional patterns
        /// </summary>
        static int CalculateInts(Point point) 
            => point switch
            {
                var (x, y) when x > 0 && y < 0 => x + y,
                var (x, y) when x < 0 && y > 0 => y + x,

                var (_, _) => throw new ArgumentException(message: "Invalid values"),
            };
    }
}
