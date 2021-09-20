using Microsoft.Extensions.DependencyInjection;
using System;

namespace DesignPatterns_RequestResponseMediatorLibrary.DependencyInjection
{
    public class MediatorServiceConfiguration
    {
        public Type MediatorImplementationType { get; private set; }
        public ServiceLifetime Lifetime { get; private set; }

        public MediatorServiceConfiguration()
        {
            MediatorImplementationType = typeof(Mediator);
            Lifetime = ServiceLifetime.Transient;
        }

        public MediatorServiceConfiguration AsSingleton()
        {
            Lifetime = ServiceLifetime.Singleton;
            return this;
        }

        public MediatorServiceConfiguration AsScoped()
        {
            Lifetime = ServiceLifetime.Scoped;
            return this;
        }

        public MediatorServiceConfiguration AsTransient()
        {
            Lifetime = ServiceLifetime.Transient;
            return this;
        }
    }
}
