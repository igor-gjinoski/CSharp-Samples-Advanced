
namespace DesignPatterns.Configurator
{
    public interface IServiceDecoratorConfigurator<in TServiceInterface>
    {
        IServiceDecoratorConfigurator<TServiceInterface> AddDecorator<TDecorator>()
            where TDecorator : TServiceInterface;

        void AddService<TService>()
            where TService : TServiceInterface;
    }
}
