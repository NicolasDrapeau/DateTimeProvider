namespace DateTimeProviderDemo;

public class PartOfTheDayServiceBefore
{
    public string GetTheOfTheDay() =>
        DateTime.UtcNow.Hour switch
        {
            >= 0 and < 12 => "morning",
            >= 12 and < 17 => "afternoon",
            _ => "evening"
        };
}
