using FluentAssertions;

namespace DateTimeProviderDemo.Tests;

public class PartOfTheDayBeforeTest
{
    [Fact]
    public void PartOfTheDay_ShouldReturnMorning_WhenItsMorning()
    {
        // Arrange
        var service = new PartOfTheDayServiceBefore();

        // Act
        var result = service.GetTheOfTheDay();

        //Assert
        result.Should().Be("morning");
    }

    [Fact]
    public void PartOfTheDay_ShouldReturnAfternoon_WhenItsAfternoon()
    {
        // Arrange
        var service = new PartOfTheDayServiceBefore();

        // Act
        var result = service.GetTheOfTheDay();

        //Assert
        result.Should().Be("afternoon");
    }

    [Fact]
    public void PartOfTheDay_ShouldReturnEvening_WhenItsEvening()
    {
        // Arrange
        var service = new PartOfTheDayServiceBefore();

        // Act
        var result = service.GetTheOfTheDay();

        //Assert
        result.Should().Be("evening");
    }
}