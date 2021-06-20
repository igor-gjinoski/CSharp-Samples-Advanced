
using System.Threading.Tasks;

namespace DesignPatterns_RequestResponseMediatorLibrary.Abstractions
{
    public interface IHandler<in TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request);
    }
}
