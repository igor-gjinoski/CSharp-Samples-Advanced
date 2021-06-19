using DependencyInjection.Abstractions;
using System;

namespace DependencyInjection.Implementations
{
    public class ScopedOperation : IScopedOperation
    {
        public Guid OperationId 
            => 
            Guid.NewGuid();
    }
}
