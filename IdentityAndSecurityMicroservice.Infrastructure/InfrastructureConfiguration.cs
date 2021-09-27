using IdentityAndSecurityMicroservice.Application;
using IdentityAndSecurityMicroservice.Infrastructure.Models;
using IdentityAndSecurityMicroservice.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace IdentityAndSecurityMicroservice.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationSettings = configuration
                .GetSection(nameof(ApplicationSettings))
                .Get<ApplicationSettings>();

            return services
                .AddIdentity(applicationSettings)
                .AddServices(applicationSettings);
        }

        private static IServiceCollection AddIdentity(
            this IServiceCollection services,
            ApplicationSettings applicationSettings)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddMongoDbStores<ApplicationUser, ApplicationRole, System.Guid>
                (
                    applicationSettings.MongoDbSettings.ConnectionString,
                    applicationSettings.ServiceSettings.ServiceName
                );

            return services;
        }

        private static IServiceCollection AddServices(
            this IServiceCollection services,
            ApplicationSettings applicationSettings)
        {
            services.AddSingleton<IMongoClient>(
                serviceProvider =>
                {
                    return new MongoClient(applicationSettings.MongoDbSettings.ConnectionString);
                });

            return services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}
