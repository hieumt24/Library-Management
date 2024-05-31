using LibraryManagement.Application.Interfaces;
using LibraryManagement.Application.Mappings;
using LibraryManagement.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagement.Application
{
    public class DependencyInjections
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<ICategoryServiceAsync, CategoryServiceAsync>();
            services.AddAutoMapper(typeof(AutoMapperProfiles));
        }
    }
}