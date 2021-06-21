using System.Threading;
using System.Threading.Tasks;

namespace CancellationTokens.Service
{
    public interface ISingletonService
    {
        Task<long> GetSomeNumbersAsync(CancellationToken token);
    }
}
