using Orleans.Runtime;
using OrleansNet6Demo.Interface.GrainExtensions;

namespace OrleansNet6Demo.Silo.WorkerService.GrainExtensions;

// ReSharper disable once ClassNeverInstantiated.Global
public class GrainInfoCollector : IGrainInfoCollector
{
    private readonly IGrainActivationContext _grainActivationContext;
    private readonly IGrainRuntime _grainRuntime;

    public GrainInfoCollector(IGrainActivationContext grainActivationContext, IGrainRuntime grainRuntime)
    {
       _grainActivationContext = grainActivationContext; 
       _grainRuntime = grainRuntime;
    }
    public Task<string> GetGrainInfo()
    {
        var grainType = _grainActivationContext.GrainType;
        var grainId = _grainActivationContext.GrainIdentity;
        var currentSiloIdentity = _grainRuntime.SiloIdentity;
        var currentSiloAddress = _grainRuntime.SiloAddress;
        
        var info = $"Type: {grainType}\n" +
                   $"Id: {grainId}\n" +
                   $"Silo Identity: {currentSiloIdentity}\n" +
                   $"Silo Address: {currentSiloAddress}";
                   
        return Task.FromResult(info);
    }
}