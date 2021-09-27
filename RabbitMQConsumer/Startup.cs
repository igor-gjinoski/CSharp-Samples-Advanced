using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MassTransit;
using RabbitMQCommon;
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
            services
                .AddMassTransit(mt =>
                {
                    var consumer = typeof(WeatherReceiver);

                    mt.AddConsumer(consumer);
                    mt.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host("localhost", "/", h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });

                        cfg.ReceiveEndpoint(consumer.FullName, endpoint =>
                        {
                            endpoint.ConfigureConsumer(context, consumer);
                        });
                    });
                }).AddMassTransitHostedService();
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
