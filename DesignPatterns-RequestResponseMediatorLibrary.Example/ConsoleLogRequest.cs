using DesignPatterns_RequestResponseMediatorLibrary.Abstractions;

namespace DesignPatterns_RequestResponseMediatorLibrary.Example
{
    public class ConsoleLogRequest : IRequest<bool>
    {
        public string Data { get; set; }
    }
}
