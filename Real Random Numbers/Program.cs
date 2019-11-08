
namespace Real_Random_Numbers
{
    using System;
    using System.Security.Cryptography;

    class Program
    {
        /// <summary>
        /// Implements a cryptographic Random Number Generator (RNG) 
        /// using the implementation provided by the cryptographic service provider (CSP). 
        /// This class cannot be inherited.
        /// </summary>
        static void Main(string[] args)
        {
            var randomNumberGenerator = RandomNumberGenerator.Create();
            
            /* Get random Int32 */
            Console.WriteLine(GenerateRandomInt32Value(randomNumberGenerator));

            /* Get random Int32 within the range between 0 and 1000 */
            /* Add one because the upper border exclude the last number */
            /* If the range is 0 - 5 */
            /* It will return a value between 0 - 4 */
            Console.WriteLine(GenerateRandomInt32ValueInRange(randomNumberGenerator, 0, 1001));
        }

        private static int GenerateRandomInt32Value(RandomNumberGenerator randomNumberGenerator)
        {
            /* Generate random array of bytes for Int32 (4 bytes) */
            var fourRandomBytes = new byte[4]; // 4 bytes = 32 bits(8 bits * 4) = Int32

            /* Fills the array with a cryptographically strong random sequence of values. */
            randomNumberGenerator.GetBytes(fourRandomBytes);

            /* Returns a 32-bit signed integer converted from four bytes at a specified position in a byte array. */
            var randomInt32Value = BitConverter.ToInt32(fourRandomBytes, 0);

            return randomInt32Value;
        }

        private static int GenerateRandomInt32ValueInRange(RandomNumberGenerator randomNumberGenerator, int min, int max)
        {
            if (min > max) throw new ArgumentOutOfRangeException(nameof(min));

            var randomInt32Value = GenerateRandomInt32Value(randomNumberGenerator);

            int diff = max - min;
            int randomInt32ValueInRange = (int)(randomInt32Value % diff); // randomInt32Value % diff;

            /* For positive number */
            return randomInt32ValueInRange >= 0
                ? randomInt32ValueInRange
                : randomInt32ValueInRange * -1;

            /* For positive or negative number */
            // return randomInt32ValueInRange
        }
    }
}
