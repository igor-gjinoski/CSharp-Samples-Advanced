using IdentityAndSecurityMicroservice.Application;
using IdentityAndSecurityMicroservice.Infrastructure.Models;
using IdentityAndSecurityMicroservice.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
                .AddIdentityServer(applicationSettings)
                .AddJSONWebTokenBearer(configuration)
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

        private static IServiceCollection AddIdentityServer(
            this IServiceCollection services,
            ApplicationSettings applicationSettings)
        {
            IdentityServerSettings identityServerSettings = new();
            services
                .AddIdentityServer()
                .AddInMemoryApiScopes(identityServerSettings.ApiScopes)
                .AddInMemoryClients(identityServerSettings.Clients);

            return services;
        }

        private static IServiceCollection AddJSONWebTokenBearer(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var secret = configuration
                .GetSection(nameof(ApplicationSettings))
                .GetValue<string>(nameof(ApplicationSettings.Secret));

            var key = System.Text.Encoding.ASCII.GetBytes(secret);

            services
                .AddAuthentication(authentication =>
                {
                    authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearer =>
                {
                    bearer.RequireHttpsMetadata = false;
                    bearer.SaveToken = true;
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

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
