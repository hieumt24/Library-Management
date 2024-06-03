using LibraryManagement.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagement.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("LibraryDb"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }
<<<<<<< HEAD
=======

            services.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
            services.AddScoped<ICategoryRepositoryAsync, CategoryRepositoryAsync>();
            services.AddScoped<IBookRepositoryAsync, BookRepositoryAsync>();
>>>>>>> d27a830a6df6256e681481fecb324138e493606f
        }
    }
}