using System;

namespace DependencyInjection.Abstractions
{
    public interface IOperation
    {
        Guid OperationId { get; }
    }
}
