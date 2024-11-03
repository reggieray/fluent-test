namespace FluentTesting;

public static class FluentTestExtensions
{
    public static FluentTest<T> Given<T>(this T subject, Action<T> setup)
    {
        return new FluentTest<T>(subject).Given(setup);
    }

    public static FluentTest<T> Given<T>(this T subject, Func<T, Task> setupAsync)
    {
        return new FluentTest<T>(subject).Given(setupAsync);
    }
    
   
}