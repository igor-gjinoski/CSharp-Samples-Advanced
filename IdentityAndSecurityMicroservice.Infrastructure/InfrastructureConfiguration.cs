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

            var identityServerSettings = configuration
                .GetSection(nameof(IdentityServerSettings))
                .Get<IdentityServerSettings>();

            return services
                .AddIdentity(applicationSettings)
                .AddIdentityServer(identityServerSettings)
                .AddJSONWebTokenBearer(applicationSettings)
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
            IdentityServerSettings identityServerSettings)
        {
            services
                .AddIdentityServer(options =>
                {
                    options.Events.RaiseSuccessEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseErrorEvents = true;
                })
                .AddAspNetIdentity<ApplicationUser>()
                .AddInMemoryApiScopes(identityServerSettings.ApiScopes)
                .AddInMemoryClients(identityServerSettings.Clients)
                .AddInMemoryIdentityResources(identityServerSettings.IdentityResources)
                .AddDeveloperSigningCredential();

            return services;
        }

        private static IServiceCollection AddJSONWebTokenBearer(
            this IServiceCollection services,
            ApplicationSettings applicationSettings)
        {
            var key = System.Text.Encoding.ASCII.GetBytes(applicationSettings.Secret);

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
