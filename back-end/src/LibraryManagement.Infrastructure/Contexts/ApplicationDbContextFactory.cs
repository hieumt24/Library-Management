//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;

//namespace LibraryManagement.Infrastructure.Contexts
//{
//    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
//    {
//        public ApplicationDbContext CreateDbContext(string[] args)
//        {
//            var config = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json")
//                .Build();
//            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
//            var connectionString = config.GetConnectionString("LibraryDb");
//            builder.UseSqlServer(connectionString);
//            return new ApplicationDbContext(builder.Options);
//        }
//    }
//}