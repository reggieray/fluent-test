namespace FluentTesting;

public class FluentTest<T>(T subject)
{
    private readonly List<Func<Task>> _givenAsyncActions = [];
    private readonly List<Func<Task>> _whenAsyncActions = [];
    private readonly List<Func<Task>> _thenAsyncActions = [];
    
    public FluentTest<T> Given(Action<T> setup)
    {
        _givenAsyncActions.Add(() => 
        {
            setup(subject);
            return Task.CompletedTask;
        });
        return this;
    }

    public FluentTest<T> Given(Func<T, Task> setupAsync)
    {
        _givenAsyncActions.Add(() => setupAsync(subject));
        return this;
    }

    public FluentTest<T> When(Action<T> action)
    {
        _whenAsyncActions.Add(() => 
        {
            action(subject);
            return Task.CompletedTask;
        });
        return this;
    }

    public FluentTest<T> When(Func<T, Task> actionAsync)
    {
        _whenAsyncActions.Add(() => actionAsync(subject));
        return this;
    }
    
    public FluentTest<T> When(string stepMsg)
    {
        return this;
    }

    public FluentTest<T> Then(Action<T> assertion)
    {
        _thenAsyncActions.Add(() => 
        {
            assertion(subject);
            return Task.CompletedTask;
        });
        return this;
    }

    public FluentTest<T> Then(Func<T, Task> assertionAsync)
    {
        _thenAsyncActions.Add(() => assertionAsync(subject));
        return this;
    }
    
    public FluentTest<T> Then(string stepMsg)
    {
        return this;
    }

    public FluentTest<T> And(Action<T> additionalAction)
    {
        var wrappedAction = () => 
        {
            additionalAction(subject);
            return Task.CompletedTask;
        };
        
        if (_thenAsyncActions.Count > 0)
        {
            _thenAsyncActions.Add(wrappedAction);
            return this;
        }
        
        if (_whenAsyncActions.Count > 0)
        {
            _whenAsyncActions.Add(wrappedAction);
            return this;
        }

        _givenAsyncActions.Add(wrappedAction);
        return this;
    }

    public FluentTest<T> And(Func<T, Task> additionalActionAsync)
    {
        if (_thenAsyncActions.Count > 0)
        {
            _thenAsyncActions.Add(() => additionalActionAsync(subject));
            return this;
        }
        
        if (_whenAsyncActions.Count > 0)
        {
            _whenAsyncActions.Add(() => additionalActionAsync(subject));
            return this;
        }
        
        _givenAsyncActions.Add(() => additionalActionAsync(subject));
        return this;
    }
    
    public FluentTest<T> And(string stepMsg)
    {
        return this;
    }
    
    public async Task RunAsync()
    {
        foreach (var action in _givenAsyncActions) await action();
        foreach (var action in _whenAsyncActions) await action();
        foreach (var action in _thenAsyncActions) await action();
    }
}