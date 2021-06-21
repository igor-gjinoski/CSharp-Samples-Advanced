using DependencyInjection.Abstractions;
using System;

namespace DependencyInjection.Implementations
{
    public class TransientOperation : ITransientOperation
    {
        private readonly Guid _operationId;

        public TransientOperation()
        {
            _operationId = Guid.NewGuid();
        }

        public Guid OperationId
        {
            get => _operationId;
        }
    }
}