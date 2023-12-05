using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using OrleansNet6Demo.Interface.GrainExtensions;
using OrleansNet6Demo.Interfaces.Grains;

namespace OrleansNet6Demo.Grains.Counter;

public class CounterGrain : Grain, ICounterGrain
{
    private readonly IPersistentState<CounterState> _counterState;
    private readonly ILogger<CounterGrain> _logger;

    public CounterGrain(
        [PersistentState("counter")]
        IPersistentState<CounterState> counterState,
        ILogger<CounterGrain> logger)
    {
        _counterState = counterState;
        _logger = logger;
    }

    public override Task OnDeactivateAsync()
    {
        _logger.LogInformation("CounterGrain {primaryKey} deactivated.", this.GetPrimaryKeyString());
        return base.OnDeactivateAsync();
    }

    public async Task Increment(int value = 1)
    {
        _counterState.State.Count += value;
        await _counterState.WriteStateAsync();
    }

    public Task<int> GetCount()
    {
        return Task.FromResult(_counterState.State.Count);
    }

    public async Task Reset()
    {
        await _counterState.ClearStateAsync();
    }

    public Task<string> GetGrainInfo()
    {
        var grinInfoCollector = this.AsReference<IGrainInfoCollector>();
        return grinInfoCollector.GetGrainInfo();
    }
}

[Serializable]
public class CounterState
{
    public int Count { get; set; } = 0;
}