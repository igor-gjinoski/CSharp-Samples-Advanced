using DependencyInjection.Abstractions;
using System;

namespace DependencyInjection.Implementations
{
    public class SingletonOperation : ISingletonOperation
    {
        private readonly Guid _operationId;

        public SingletonOperation()
        {
            _operationId = Guid.NewGuid();
        }

        public Guid OperationId
        {
            get => _operationId;
        }
    }
}