using System;

namespace DependencyInjection.Abstractions
{
    public interface IOperationService
    {
        Guid GetScopedOperationId();
        Guid GetSingletonOperationId();
        Guid GetTransientOperationId();
    }
}
