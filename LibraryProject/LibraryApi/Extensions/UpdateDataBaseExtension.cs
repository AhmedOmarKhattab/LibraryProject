using DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Extensions
{
    public static class UpdateDataBaseExtension
    {
        public  async static Task< WebApplication> UpdateDatabaseExtension(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbcontext = services.GetService<LibraryContext>();
                await dbcontext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occured during apply migration");
            }
            return app;

        }

    }
}
