using DependencyInjection.Abstractions;
using System;

namespace DependencyInjection.Implementations
{
    public class SingletonOperation : ISingletonOperation
    {
        public Guid OperationId
            =>
            Guid.NewGuid();
    }
}