
namespace EscapeSpecialCharacters
{
    using System.IO;
    using System.CodeDom;
    using System.CodeDom.Compiler;

    class Program
    {
        static void Main(string[] args)
        {
        }

        /// <summary>
        /// ToLiteral
        /// Using CodeDomProvider
        /// escape special characters from text and convert it to escaped C# string
        /// </summary>
        private static string ToLiteral(string UrlSet)
        {
            using var writer = new StringWriter();
            using var provider = CodeDomProvider.CreateProvider("CSharp");

            provider.GenerateCodeFromExpression(new CodePrimitiveExpression(UrlSet), writer, null);

            return writer.ToString();
        }
    }
}
