using System.Threading.Tasks;

namespace DesignPatterns_RequestResponseMediatorLibrary.Abstractions
{
    public interface IMediator
    {
        Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request);
    }
}
