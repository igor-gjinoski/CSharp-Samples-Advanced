using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using DesignPatterns_RequestResponseMediatorLibrary.Abstractions;

namespace DesignPatterns_RequestResponseMediatorLibrary
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ConcurrentDictionary<Type, Type> _requestHandlers = new();

        public Mediator(IServiceProvider serviceProvider, ConcurrentDictionary<Type, Type> requestHandlers)
        {
            _serviceProvider = serviceProvider;
            _requestHandlers = requestHandlers;
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken)
        {
            var requestType = request.GetType();
            if (!_requestHandlers.ContainsKey(requestType))
            {
                throw new Exception($"No handler to handle request of type: {requestType.Name}");
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                _requestHandlers.TryGetValue(requestType, out var requestHandlerType);

                var handler = scope.ServiceProvider.GetRequiredService(requestHandlerType);

                return await
                    (Task<TResponse>)handler
                    .GetType()
                    .GetMethod("HandleAsync")
                    .Invoke(handler, new object[] { request, cancellationToken });
            }
        }
    }
}
