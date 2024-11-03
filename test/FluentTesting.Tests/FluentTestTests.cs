namespace FluentTesting.Tests;

using Xunit;
using System.Threading.Tasks;

public class FluentTestTests
{
    private readonly TestRecorder _recorder;
    private readonly FluentTest<TestRecorder> _fluentTest;

    public FluentTestTests()
    {
        _recorder = new TestRecorder();
        _fluentTest = new FluentTest<TestRecorder>(_recorder);
    }

    [Fact]
    public async Task RunAsync_ExecutesGivenWhenThenInOrder()
    {
        // Arrange
        _fluentTest
            .Given(r => r.Record("Given"))
            .When(r => r.Record("When"))
            .Then(r => r.Record("Then"));

        // Act
        await _fluentTest.RunAsync();

        // Assert
        Assert.Equal(["Given", "When", "Then"], _recorder.Steps);
    }

    [Fact]
    public async Task RunAsync_ExecutesAsyncGivenWhenThenInOrder()
    {
        // Arrange
        _fluentTest
            .Given(async r => await r.RecordAsync("Given"))
            .When(async r => await r.RecordAsync("When"))
            .Then(async r => await r.RecordAsync("Then"));

        // Act
        await _fluentTest.RunAsync();

        // Assert
        Assert.Equal(["Given", "When", "Then"], _recorder.Steps);
    }

    [Fact]
    public async Task And_AppendsToTheCorrectPhase_GivenPhase()
    {
        // Arrange
        _fluentTest
            .Given(r => r.Record("Given"))
            .And(r => r.Record("AndGiven"));

        // Act
        await _fluentTest.RunAsync();

        // Assert
        Assert.Equal(["Given", "AndGiven"], _recorder.Steps);
    }

    [Fact]
    public async Task And_AppendsToTheCorrectPhase_WhenPhase()
    {
        // Arrange
        _fluentTest
            .When(r => r.Record("When"))
            .And(r => r.Record("AndWhen"));

        // Act
        await _fluentTest.RunAsync();

        // Assert
        Assert.Equal(["When", "AndWhen"], _recorder.Steps);
    }

    [Fact]
    public async Task And_AppendsToTheCorrectPhase_ThenPhase()
    {
        // Arrange
        _fluentTest
            .Then(r => r.Record("Then"))
            .And(r => r.Record("AndThen"));

        // Act
        await _fluentTest.RunAsync();

        // Assert
        Assert.Equal(["Then", "AndThen"], _recorder.Steps);
    }

    [Fact]
    public async Task MixedAsyncAndSyncActions_ExecuteInCorrectOrder()
    {
        // Arrange
        _fluentTest
            .Given(async r => await r.RecordAsync("GivenAsync"))
            .Given(r => r.Record("GivenSync"))
            .When(async r => await r.RecordAsync("WhenAsync"))
            .When(r => r.Record("WhenSync"))
            .Then(async r => await r.RecordAsync("ThenAsync"))
            .Then(r => r.Record("ThenSync"));
            

        // Act
        await _fluentTest.RunAsync();

        // Assert
        Assert.Equal(
            ["GivenAsync", "GivenSync", "WhenAsync", "WhenSync", "ThenAsync", "ThenSync"],
            _recorder.Steps
        );
    }

    [Fact]
    public async Task StepMessages_DoNotAffectExecution()
    {
        // Arrange
        _fluentTest
            .Given(r => r.Record("Given"))
            .When("Intermediate step description")
            .When(r => r.Record("When"))
            .Then("Final step description")
            .Then(r => r.Record("Then"));

        // Act
        await _fluentTest.RunAsync();

        // Assert
        Assert.Equal(["Given", "When", "Then"], _recorder.Steps);
    }

    [Fact]
    public async Task EmptyRunAsync_DoesNotThrowException()
    {
        // Arrange & Act
        var exception = await Record.ExceptionAsync(_fluentTest.RunAsync);

        // Assert
        Assert.Null(exception);
    }
}
