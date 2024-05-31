//using LibraryManagement.Domain.Common.Repositories;
//using LibraryManagement.Infrastructure.Contexts;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//namespace LibraryManagement.Infrastructure
//{
//    public class DependencyInjections
//    {
//        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
//        {
//            var connectionString = configuration.GetConnectionString("LibraryDb");

//            services.AddDbContext<ApplicationDbContext>(options =>
//               options.UseSqlServer(connectionString,
//               x => x.MigrationsAssembly("LibraryManagement.Infrastructure")));
//        }

//        public static void MigrateDatabase(IServiceProvider serviceProvider)
//        {
//            var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>();

//            using (var dbContext = new ApplicationDbContext(dbContextOptions))
//            {
//                dbContext.Database.Migrate();
//            }
//        }
//    }
//}