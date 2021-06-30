using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DesignPatterns_Repository.Data;
using DesignPatterns_Repository.Implementation.UnitOfWork.CustomerUnitOfWork;
using DesignPatterns_Repository.Implementation.SeviceCollectionExtensions;
using DesignPatterns_Repository.Repository.CustomerRepository;

namespace DesignPatterns_Repository
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
            services.AddDbContext<ApplicationDbContext>(config =>
                config.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddUnitOfWork<CustomerUnitOfWork, ApplicationDbContext>();

            services
                .AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
