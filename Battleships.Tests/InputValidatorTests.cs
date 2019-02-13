using Battleships.Utils;
using NUnit.Framework;

namespace Battleships.Tests
{
    public class InputValidatorTests
    {
        [TestCase("A1")]
        [TestCase("A10")]
        [TestCase("J1")]
        [TestCase("J10")]
        public void ValidateInput_WhenProperInput_ShouldReturnTrue(string input)
        {
            var sut = new InputValidator();
            var result = sut.IsInputValid(input);

            Assert.That(result, Is.True);
        }

        [TestCase(null)]
        [TestCase("A")]
        [TestCase("J1343")]
        [TestCase("J12")]
        [TestCase("K1")]
        public void ValidateInput_WhenNotValidInput_ShouldReturnFalse(string input)
        {
            var sut = new InputValidator();
            var result = sut.IsInputValid(input);

            Assert.That(result, Is.False);
        }
    }
}