using FluentAssertions;
using System;
using Xunit;

namespace Calculator.UnitTests
{
    public class CalculatorTests
    {
        private Calculator _sut;
        public CalculatorTests()
        {
            _sut = new Calculator();
        }

        [Fact]
        public void Add_ReturnsSum_WhenCalledWithInput()
        {
            // Arrange
            double a = 1;
            double b = 1;
            double expected = 2;

            // Act
            var result = _sut.Add(a, b);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Subtract_ReturnsDifference_WhenCalledWithInput()
        {
            // Arrange
            double a = 1;
            double b = 1;
            double expected = 0;

            // Act
            var result = _sut.Subtract(a, b);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 2, 2)]
        [InlineData(2, 2, 4)]
        [InlineData(5, 2, 10)]
        public void Multiply_ReturnsProduct_WhenCalledWithInput(double a, double b, double expected)
        {
            // Arrange

            // Act
            var result = _sut.Multiply(a, b);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Divide_ReturnsQuotient_WhenCalledWithValidInput()
        {
            // Arrange
            double a = 4;
            double b = 2;
            double expected = 2;

            // Act
            var result = _sut.Divide(a, b);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Divide_ThrowsException_WhenCalledWithZeroAsDivisor()
        {
            // Arrange
            double a = 1;
            double b = 0;
            string expectedExceptionMessage = "Cannot divide by zero.";

            // Act
            var exception = Assert.Throws<ArgumentException>(() => _sut.Divide(a, b));

            // Assert
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }

        // The below shows difference when using FluentAssertion.

        [Fact]
        public void Add_ReturnsSum_WhenCalledWithInput_FluentAssertion()
        {
            // Arrange
            double a = 1;
            double b = 1;
            double expected = 2;

            // Act
            var result = _sut.Add(a, b);

            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void Divide_ThrowsException_WhenCalledWithZeroAsDivisor_FluentAssertion()
        {
            // Arrange
            double a = 1;
            double b = 0;
            string expectedExceptionMessage = "Cannot divide by zero.";

            // Act
            var action = () => _sut.Divide(a, b);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage(expectedExceptionMessage);
        }
    }
}