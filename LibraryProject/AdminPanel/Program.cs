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
        public static void Main(string[] args)
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
