namespace FluentTesting;

public static class FluentTestExtensions
{
    public static FluentTest<T> Given<T>(this T subject, string message)
    {
        return new FluentTest<T>(subject);
    }
    
    public static FluentTest<T> Given<T>(this T subject, Action<T> setup)
    {
        return new FluentTest<T>(subject).Given(setup);
    }

    public static FluentTest<T> When<T>(this FluentTest<T> fluentTest, Action<T> action)
    {
        return fluentTest.When(action);
    }
    
    public static FluentTest<T> When<T>(this FluentTest<T> fluentTest, string message)
    {
        return fluentTest;
    }

    public static FluentTest<T> Then<T>(this FluentTest<T> fluentTest, Action<T> assertion)
    {
        return fluentTest.Then(assertion);
    }
    
    public static FluentTest<T> Then<T>(this FluentTest<T> fluentTest, string message)
    {
        return fluentTest;
    }

    public static FluentTest<T> And<T>(this FluentTest<T> fluentTest, Action<T> additionalAction)
    {
        return fluentTest.And(additionalAction);
    }
    
    public static FluentTest<T> And<T>(this FluentTest<T> fluentTest, string message)
    {
        return fluentTest;
    }

    public static void Test<T>(this FluentTest<T> fluentTest)
    {
        fluentTest.Test();
    }
}