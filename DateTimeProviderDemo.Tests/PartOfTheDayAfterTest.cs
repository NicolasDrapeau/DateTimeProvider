using FluentAssertions;
using NSubstitute;

namespace DateTimeProviderDemo.Tests;

public class PartOfTheDayAfterTest
{
    private readonly PartOfTheDayServiceAfter _service;
    private readonly IDateTimeProvider _dateTimeProvider = Substitute.For<IDateTimeProvider>();

    public PartOfTheDayAfterTest()
    {
        _service = new PartOfTheDayServiceAfter(_dateTimeProvider);
    }


    [Fact]
    public void PartOfTheDay_ShouldReturnMorning_WhenItsMorning()
    {
        // Arrange
        var date = new DateTime(2024, 11, 08, 11, 00, 00);
        _dateTimeProvider.Now.Returns(date);

        // Act
        var result = _service.GetTheOfTheDay();

        //Assert
        result.Should().Be("morning");
    }

    [Fact]
    public void PartOfTheDay_ShouldReturnAfternoon_WhenItsAfternoon()
    {
        // Arrange
        var date = new DateTime(2024, 11, 08, 13, 00, 00);
        _dateTimeProvider.Now.Returns(date);

        // Act
        var result = _service.GetTheOfTheDay();

        //Assert
        result.Should().Be("afternoon");
    }

    [Fact]
    public void PartOfTheDay_ShouldReturnEvening_WhenItsEvening()
    {
        // Arrange
        var date = new DateTime(2024, 11, 08, 18, 00, 00);
        _dateTimeProvider.Now.Returns(date);

        // Act
        var result = _service.GetTheOfTheDay();

        //Assert
        result.Should().Be("evening");
    }
}