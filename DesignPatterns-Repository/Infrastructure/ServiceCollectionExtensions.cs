using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DesignPatterns_Repository.Implementation.UnitOfWork;
using DesignPatterns_Repository.UnitOfWork;

namespace DesignPatterns_Repository.Implementation.SeviceCollectionExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWork<TUnitOfWork, TDbContext>(this IServiceCollection services)
            where TUnitOfWork : UnitOfWork<TDbContext>
            where TDbContext : DbContext
        {
            return services
                .AddScoped<IUnitOfWork<TDbContext>, TUnitOfWork>();
        }
    }
}
