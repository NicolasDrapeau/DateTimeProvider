namespace DateTimeProviderDemo;

public interface IDateTimeProvider
{
    public DateTimeOffset Now { get; }
}
