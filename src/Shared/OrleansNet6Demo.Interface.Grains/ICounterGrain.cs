using System.Threading.Tasks;
using Orleans;

namespace OrleansNet6Demo.Interfaces.Grains
{
   public interface ICounterGrain : IGrainWithStringKey
   {
      public Task Increment(int value = 1);
      public Task<int> GetCount();
      public Task Reset();

      /// <summary>
      /// Delegate method for calling Grain Extension method from Orleans client
      /// </summary>
      /// <returns></returns>
      public Task<string> GetGrainInfo();
   }
}