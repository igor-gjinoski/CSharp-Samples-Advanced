using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationTokens.Service
{
    public class SingletonService : ISingletonService
    {
        public async Task<long> GetSomeNumbersAsync(CancellationToken token)
        {
            /*
             * Some Very Slow Operation
             */
            var list = new List<int>();
            for (int index = 0; index < 1000000000; index++)
            {
                list.Add(index);
            }
            return await Task.FromResult(1);
        }
    }
}
