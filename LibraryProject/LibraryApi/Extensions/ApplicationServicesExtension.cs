using DAL.Interfaces;
using DLL.Repositories;
using LibraryApi.Profiles;

namespace LibraryApi.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IBookRepository), typeof(BookRepository));
            Services.AddScoped(typeof(IGenerRepository), typeof(GenerRepository));
            Services.AddScoped(typeof(IAuthorRepository), typeof(AuthorRepository));

            Services.AddAutoMapper(m => m.AddProfile(new BookProfile()));
            Services.AddAutoMapper(m => m.AddProfile(new GenerProfile()));
            Services.AddAutoMapper(m => m.AddProfile(new AuthorProfile()));

            return Services;
        }

    }
}
