using Dapper.Repository.Domain;
using Dapper.Repository.Internal.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Dapper.Repository
{
    public static class ServiceRegistrar
    {
        public static void AddRepository(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IDbConnectionFactory>(_ => new DefaultDbConnectionFactory(connectionString));
            services.AddTransient<IRepository, DapperRepository>();
        }
        
        public static void AddRepository(this IServiceCollection services, IDbConnectionFactory factory)
        {
            services.AddTransient(_ => factory);
            services.AddTransient<IRepository, DapperRepository>();
        }
    }
}