
namespace String_Intern_Sample
{
    using System;

    /// <summary>
    /// Compile time: 
    /// Two equal strings always point to same address in memory
    /// For Runtime:
    /// We can check if memory address already contains a cell with the same value
    /// Syntax: String.Intern();
    /// </summary>
    class Program
    {
        unsafe static void Main(string[] args)
        {
            /* Two strings with same value point to the same address in memory */
            string v1 = "Some value";
            string v2 = "Some value";

            /* Get the references of the two strings */
            var ref_v1 = __makeref(v1);
            var ref_v2 = __makeref(v2);

            /* Print the memory address in Hexadecimal */
            Console.WriteLine(
                $"Address of first String:{(**(IntPtr**)&ref_v1).ToString("X")} " +
                $"\nAddress of second String: {(**(IntPtr**)&ref_v2).ToString("X")}");

            /* Two strings with same value point to the same address in memory */
            /* First value is set Compile time */
            /* Second value is set Runtime */
            /* We use String.Intern to check if there is an address containing the same value */
            v1 = "Some new value";
            v2 = String.Intern(Console.ReadLine());

            /* Get the references of the two strings */
            ref_v1 = __makeref(v1);
            ref_v2 = __makeref(v2);

            /* Print the memory address in Hexadecimal */
            Console.WriteLine(
                $"Address of first String:{(**(IntPtr**)&ref_v1).ToString("X")} " +
                $"\nAddress of second String: {(**(IntPtr**)&ref_v2).ToString("X")}");
        }
    }
}

