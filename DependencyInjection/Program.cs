﻿using System;
using DependencyInjection.Abstractions;
using DependencyInjection.Implementations;
using DependencyInjection.Writer;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
    public class Program
    {
        public class OperationService : IOperationService
        {
            public IScopedOperation ScopedOperation { get; }
            private ISingletonOperation SingletonOperation { get; }
            private ITransientOperation TransientOperation { get; }

            public OperationService(
                IScopedOperation scopedOperation,
                ISingletonOperation singletonOperation,
                ITransientOperation transientOperation)
            {
                ScopedOperation = scopedOperation;
                SingletonOperation = singletonOperation;
                TransientOperation = transientOperation;
            }

            public Guid GetScopedOperationId() 
                => ScopedOperation.OperationId;

            public Guid GetSingletonOperationId()
                => SingletonOperation.OperationId;

            public Guid GetTransientOperationId() 
                => TransientOperation.OperationId;
        }


        public static IServiceProvider BuildServiceProvider(IServiceCollection services)
        {
            services.AddScoped<IScopedOperation, ScopedOperation>();
            services.AddSingleton<ISingletonOperation, SingletonOperation>();
            services.AddTransient<ITransientOperation, TransientOperation>();

            return services.BuildServiceProvider();
        }

        static void Main()
        {
            IServiceCollection services = new ServiceCollection();
            var serviceProvider = BuildServiceProvider(services);
        }
    }
}
