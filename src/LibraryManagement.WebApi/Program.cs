using LibraryManagement.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddControllers()
          .AddJsonOptions(options =>
          {
              options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
              options.JsonSerializerOptions.WriteIndented = true;
          });
<<<<<<< HEAD

        builder.Services.AddSwaggerGen(options =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
            var securitySchema = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter JWT Bearer",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
            options.AddSecurityDefinition("Bearer", securitySchema);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securitySchema, new[] { "Bearer" } }
            });
        });
=======
>>>>>>> d27a830a6df6256e681481fecb324138e493606f
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        //builder.Services.AddDbContext<ApplicationDbContext>(options =>
        //{
        //    options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDb"));
        //});
        LibraryManagement.Application.DependencyInjections.Configure(builder.Services);
        LibraryManagement.Infrastructure.DependencyInjections.ConfigureServices(builder.Services, builder.Configuration);
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}