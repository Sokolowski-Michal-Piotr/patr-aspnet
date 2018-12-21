using System;
using System.Text;

namespace WebApi
{
    public static partial class Methods
    {
        /// <summary>
        /// Prints Fizz and/or Buzz if the value is divisible by 2 and/or 3.
        /// </summary>
        /// <param name="value">Value to be tested. It must be in the range of 0-1000.</param>
        /// <returns>Fizz, Buzz, FizzBuzz or empty string.</returns>
        public static string FizzBuzz(int value)
        {
            if (value < 0 || value > 1000)
                throw new ArgumentOutOfRangeException();

            var result = new StringBuilder();

            if (value % 2 == 0)
                result.Append("Fizz");
            if (value % 3 == 0)
                result.Append("Buzz");

            return result.ToString();
        }
    }
}
