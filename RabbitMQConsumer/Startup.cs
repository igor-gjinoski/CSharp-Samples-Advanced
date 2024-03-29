using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQCommon;
using RabbitMQConsumer.Messages;
using RabbitMQConsumer.Messaging;

namespace RabbitMQConsumer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // RabbitMQ
            services.Configure<RabbitMqConfiguration>(
                Configuration.GetSection("RabbitMqConfiguration"));
            services.AddHostedService<WeatherReceiver>();

            // RabbitMQ with MassTransit
            services.AddMassTransitMessaging(
                new System.Type[] 
                { 
                    typeof(WeatherReceiver_MassTrasit) 
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection()
               .UseRouting()
               .UseAuthorization()
               .UseEndpoints(endpoints =>
               {
                   endpoints.MapControllers();
               });
        }
    }
}
