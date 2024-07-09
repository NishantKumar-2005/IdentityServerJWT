using Squib.UserService.API.config;
using Squib.UserService.API.Repository;
using Squib.UserService.API.Service;

namespace Squib.UserService.API
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSquibUserService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionString>(configuration.GetSection("ConnectionStrings"));
            services.AddSingleton<IUserRepo, UserRepo>();
            services.AddSingleton<IUSER_Service, UserServi>();
            return services;
        }
    }
}

