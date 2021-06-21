using DependencyInjection.Abstractions;
using System;

namespace DependencyInjection.Implementations
{
    public class ScopedOperation : IScopedOperation
    {
        private readonly Guid _operationId;

        public ScopedOperation()
        {
            _operationId = Guid.NewGuid();
        }

        public Guid OperationId
        {
            get => _operationId;
        }
    }
}
