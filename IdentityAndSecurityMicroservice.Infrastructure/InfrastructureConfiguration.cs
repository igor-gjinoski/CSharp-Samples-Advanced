using IdentityAndSecurityMicroservice.Application;
using IdentityAndSecurityMicroservice.MongoDB.Entities;
using IdentityAndSecurityMicroservice.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace IdentityAndSecurityMicroservice.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddIdentity(configuration)
                .AddServices();
        }


        private static IServiceCollection AddIdentity(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

            var applicationSettings = configuration
                .GetSection(nameof(ApplicationSettings))
                .Get<ApplicationSettings>();

            services
                .AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddMongoDbStores<ApplicationUser, ApplicationRole, System.Guid>
                (
                    applicationSettings.MongoDbSettings.ConnectionString,
                    applicationSettings.ServiceSettings.ServiceName
                );

            return services;
        }


        private static IServiceCollection AddServices(
            this IServiceCollection services)
        {
            return services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}
