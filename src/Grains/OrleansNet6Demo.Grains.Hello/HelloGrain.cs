using Microsoft.Extensions.Logging;
using Orleans;
using OrleansNet6Demo.Interface.GrainExtensions;
using OrleansNet6Demo.Interfaces.Grains;

namespace OrleansNet6Demo.Grains.Hello;

public class HelloGrain : Grain, IHelloGrain
{
    private readonly ILogger<HelloGrain> _logger;

    public HelloGrain(ILogger<HelloGrain> logger)
    {
        _logger = logger;
    }
    
    public Task<string> SayHello(string greeting)
    {
        return Task.FromResult($"You said: '{greeting}', I say: Hello!");
    }
    
    public Task<string> GetGrainInfo()
    {
        var grinInfoCollector = this.AsReference<IGrainInfoCollector>();
        return grinInfoCollector.GetGrainInfo();
    }
}