using System;

namespace DesignPatterns.Configurator
{
    public interface IServiceDecoratorBuilder<out TServiceInterface>
    {
        TServiceInterface Build(IServiceProvider serviceProvider);
    }
}
