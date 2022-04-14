using FluentAssertions;
using InventoryApplication.Interfaces;
using NSubstitute;
using System;
using Xunit;

namespace InventoryApplication.Tests
{
    public class InventoryServiceTests
    {
        private readonly InventoryService _sut;
        private readonly ICategoriesRepository _categoriesRepository = Substitute.For<ICategoriesRepository>();
        private readonly IDateTimeProvider _dateTimeProvider = Substitute.For<IDateTimeProvider>();
        private readonly IInventoryDataAccess _inventoryDataAccess = Substitute.For<IInventoryDataAccess>();

        private const string TEST_NAME = "Test Name";
        private const string TEST_DESCRIPTION = "Test Description";
        private const string TEST_CATEGORY = "Test Category";
        private const string CLOTHING_CATEGORY = "Clothing";
        private const double TEST_PRICE = 10d;
        private static readonly DateTime TEST_DATE = new DateTime(2000, 1, 10);
        private static DateTime ONE_DAY_EARLIER => TEST_DATE.AddDays(-1);
        private static DateTime ONE_DAY_LATER => TEST_DATE.AddDays(1);
        private static DateTime TEN_DAYS_LATER => TEST_DATE.AddDays(10);
        private static DateTime TWENTY_DAYS_LATER => TEST_DATE.AddDays(20);

        public InventoryServiceTests()
        {
            _sut = new InventoryService(_categoriesRepository, _dateTimeProvider, _inventoryDataAccess);
        }

        [Fact]
        public void TryAddItem_ReturnsTrue_WhenAllInputsAreValid()
        {
            // Arrange
            _categoriesRepository.GetByName(TEST_CATEGORY).Returns(new Category { Id = 1, Name = TEST_CATEGORY });
            _dateTimeProvider.GetCurrentDate().Returns(TEST_DATE);

            // Act
            var result = _sut.TryAddItem(TEST_NAME, TEST_DESCRIPTION, TEST_CATEGORY, TEST_PRICE, ONE_DAY_LATER);

            // Assert
            _inventoryDataAccess.Received(1).AddInventory(Arg.Any<InventoryItem>());
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("", TEST_DESCRIPTION, TEST_CATEGORY)]
        [InlineData(TEST_NAME, "", TEST_CATEGORY)]
        [InlineData(TEST_NAME, TEST_DESCRIPTION, "")]
        public void TryAddItem_ReturnsFalse_WhenAnyStringInputIsNullOrWhitespace(string name, string description, string category)
        {
            // Arrange
            _categoriesRepository.GetByName(category).Returns(new Category { Id = 1, Name = category });
            _dateTimeProvider.GetCurrentDate().Returns(TEST_DATE);

            // Act
            var result = _sut.TryAddItem(name, description, category, TEST_PRICE, ONE_DAY_LATER);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void TryAddItem_ReturnsFalse_WhenCategoryDoesNotExist()
        {
            // Arrange
            _categoriesRepository.GetByName(TEST_CATEGORY).Returns((Category?)null);
            _dateTimeProvider.GetCurrentDate().Returns(TEST_DATE);

            // Act
            var result = _sut.TryAddItem(TEST_NAME, TEST_DESCRIPTION, TEST_CATEGORY, TEST_PRICE, ONE_DAY_LATER);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void TryAddItem_ReturnsFalse_WhenPriceIsZeroOrNegative()
        {
            // Arrange
            _categoriesRepository.GetByName(TEST_CATEGORY).Returns(new Category { Id = 1, Name = TEST_CATEGORY });
            _dateTimeProvider.GetCurrentDate().Returns(TEST_DATE);

            // Act
            var result = _sut.TryAddItem(TEST_NAME, TEST_DESCRIPTION, TEST_CATEGORY, -1, ONE_DAY_LATER);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void TryAddItem_ReturnsFalse_WhenSaleStartIsAPriorDate()
        {
            // Arrange
            _categoriesRepository.GetByName(TEST_CATEGORY).Returns(new Category { Id = 1, Name = TEST_CATEGORY });
            _dateTimeProvider.GetCurrentDate().Returns(TEST_DATE);

            // Act
            var result = _sut.TryAddItem(TEST_NAME, TEST_DESCRIPTION, TEST_CATEGORY, TEST_PRICE, ONE_DAY_EARLIER);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void TryAddItem_ReturnsFalse_WhenSaleStartIsMoreThanTwoWeeksLaterForNonClothingItem()
        {
            // Arrange
            _categoriesRepository.GetByName(TEST_CATEGORY).Returns(new Category { Id = 1, Name = TEST_CATEGORY });
            _dateTimeProvider.GetCurrentDate().Returns(TEST_DATE);

            // Act
            var result = _sut.TryAddItem(TEST_NAME, TEST_DESCRIPTION, TEST_CATEGORY, TEST_PRICE, TWENTY_DAYS_LATER);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void TryAddItem_ReturnsFalse_WhenSaleStartIsMoreThanOneWeekLaterForClothingItem()
        {
            // Arrange
            _categoriesRepository.GetByName(CLOTHING_CATEGORY).Returns(new Category { Id = 1, Name = CLOTHING_CATEGORY });
            _dateTimeProvider.GetCurrentDate().Returns(TEST_DATE);

            // Act
            var result = _sut.TryAddItem(TEST_NAME, TEST_DESCRIPTION, CLOTHING_CATEGORY, TEST_PRICE, TWENTY_DAYS_LATER);

            // Assert
            result.Should().BeFalse();
        }
    }
}