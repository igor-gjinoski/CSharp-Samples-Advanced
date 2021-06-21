using System;
using System.Threading;
using System.Threading.Tasks;
using CancellationTokens.Service;
using Microsoft.Extensions.DependencyInjection;

namespace CancellationTokens
{
    class Program
    {
        static async Task Main()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ISingletonService, SingletonService>()
                .BuildServiceProvider();

            var singletonService = serviceProvider.GetService<ISingletonService>();

            var cancellationTokenSource = new CancellationTokenSource(
                new TimeSpan(0, 0, 5));

            await CancelRequestWithWhileLoop(singletonService, cancellationTokenSource.Token);
            await CancelRequestWithThrowIf(singletonService, cancellationTokenSource.Token);
        }


        static async Task<long> CancelRequestWithWhileLoop(ISingletonService singleton, CancellationToken cancellationToken)
        {
            var result = await singleton.GetSomeNumbersAsync(cancellationToken);

            while (!cancellationToken.IsCancellationRequested)
            {
                /*
                 * Do Some Work
                 */
                return result;
            }
            throw new TaskCanceledException();
        }


        static async Task<long> CancelRequestWithThrowIf(ISingletonService singleton, CancellationToken cancellationToken)
        {
            long result = await singleton.GetSomeNumbersAsync(cancellationToken);

            /*
             * throw OperationCanceledException if Cancelled
             */
            cancellationToken.ThrowIfCancellationRequested();

            return result;
        }
    }
}
