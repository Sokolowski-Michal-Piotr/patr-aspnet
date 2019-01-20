using System;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    public class FizzBuzz
    {
        delegate string SystemUnderTest(int value);
        readonly SystemUnderTest systemUnderTest = WebApi.Methods.FizzBuzz;

        [TestCase(0, "FizzBuzz")]
        [TestCase(1, "")]
        [TestCase(2, "Fizz")]
        [TestCase(3, "Buzz")]
        [TestCase(6, "FizzBuzz")]
        [TestCase(1000, "Fizz")]
        public void InRange(int value, string expected)
        {
            Assert.That(systemUnderTest(value), Is.EqualTo(expected));
        }

        [TestCase(-1)]
        [TestCase(1001)]
        public void OutOfRange(int value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(delegate { systemUnderTest(value); });
        }
    }
}