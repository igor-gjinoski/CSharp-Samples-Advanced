using DependencyInjection.Abstractions;
using System;

namespace DependencyInjection.Implementations
{
    public class TransientOperation : ITransientOperation
    {
        public Guid OperationId
            =>
            Guid.NewGuid();
    }
}