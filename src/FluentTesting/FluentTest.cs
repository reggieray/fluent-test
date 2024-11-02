namespace FluentTesting;

public class FluentTest<T>(T subject)
{
    private readonly List<Action> _givenActions = [];
    private readonly List<Action> _whenActions = [];
    private readonly List<Action> _thenActions = [];

    public FluentTest<T> Given(Action<T> setup)
    {
        _givenActions.Add(() => setup(subject));
        return this;
    }

    public FluentTest<T> When(Action<T> action)
    {
        _whenActions.Add(() => action(subject));
        return this;
    }

    public FluentTest<T> Then(Action<T> assertion)
    {
        _thenActions.Add(() => assertion(subject));
        return this;
    }

    public FluentTest<T> And(Action<T> additionalAction)
    {
        if (_thenActions.Count > 0)
        {
            _thenActions.Add(() => additionalAction(subject));
            return this;
        }
        
        if (_whenActions.Count > 0)
        {
            _whenActions.Add(() => additionalAction(subject));
            return this;
        }
        
        _givenActions.Add(() => additionalAction(subject));
        return this;
    }

    public void Test()
    {
        foreach (var action in _givenActions) action();
        foreach (var action in _whenActions) action();
        foreach (var action in _thenActions) action();
    }
}