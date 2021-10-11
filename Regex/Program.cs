using System.Text.RegularExpressions;

namespace Regexes
{
    class Program
    {
        static void Main()
        {
        }
        private static void UseRegexWithMatchCollection()
        {
            string text = "A regular expression is used to check if a string matches a pattern or not";
            MatchCollection matchCollection = Regex.Matches(text, "[a-zA-Z0-9 ]+");

            foreach (Match match in matchCollection)
            {
            }
        }
    }
}
