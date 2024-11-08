namespace DateTimeProviderDemo;

public class PartOfTheDayServiceAfter
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public PartOfTheDayServiceAfter(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string GetTheOfTheDay() =>
        _dateTimeProvider.Now.Hour switch
        {
            >= 0 and < 12 => "morning",
            >= 12 and < 17 => "afternoon",
            _ => "evening"
        };
}
