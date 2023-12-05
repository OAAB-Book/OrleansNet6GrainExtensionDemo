using System.Threading.Tasks;
using Orleans.Runtime;

namespace OrleansNet6Demo.Interface.GrainExtensions
{
    public interface IGrainInfoCollector : IGrainExtension
    {
        Task<string> GetGrainInfo();
    }
}