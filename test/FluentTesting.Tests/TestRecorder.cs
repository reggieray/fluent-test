namespace FluentTesting.Tests;

public class TestRecorder
{
    public List<string> Steps { get; } = [];

    public void Record(string step) => Steps.Add(step);
    public Task RecordAsync(string step)
    {
        Steps.Add(step);
        return Task.CompletedTask;
    }
}