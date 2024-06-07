using LibraryManagement.Application.Common.Services;
using LibraryManagement.Application.Mappings;
using LibraryManagement.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagement.Application
{
    public class ServiceExtensions
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<ICategoryServiceAsync, CategoryServiceAsync>();
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            services.AddScoped<IBookServiceAsync, BookServiceAsync>();
            services.AddScoped<IBookBorrowRequestServiceAsync, BookBorrowRequestServiceAsync>();
            services.AddScoped<IBookBorrowRequestDetailsServiceAsync, BookBorrowRequestDetailsServiceAysnc>();
        }
    }
}