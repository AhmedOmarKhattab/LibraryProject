using AdminPanel.Profiles;
using DAL.Interfaces;
using DAL.Models;
using DLL.Data;
using DLL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<LibraryContext>(option=>
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IAdminRepository<Book>,AdminRepository<Book>>();
            builder.Services.AddScoped<IAdminRepository<Gener>, AdminRepository<Gener>>();
            builder.Services.AddScoped<IAdminRepository<Author>, AdminRepository<Author>>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(m=>m.AddProfile(new BookProfile()));
            builder.Services.AddAutoMapper(m => m.AddProfile(new GenerProfile()));
            builder.Services.AddAutoMapper(m => m.AddProfile(new AuthorProfile()));


            var app = builder.Build();
         
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
        

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
