using System.Threading.Tasks;
using Orleans;

namespace OrleansNet6Demo.Interfaces.Grains
{
    public interface IHelloGrain : IGrainWithIntegerKey
    {
        Task<string> SayHello(string greeting);

        /// <summary>
        /// Delegate method for calling Grain Extension method from Orleans client
        /// </summary>
        /// <returns></returns>
        public Task<string> GetGrainInfo();
    }
}