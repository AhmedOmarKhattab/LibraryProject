
using DAL.Interfaces;
using DAL.Models;
using DLL.Data;
using DLL.Repositories;
using LibraryApi.Profiles;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<LibraryContext>(option=>
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped(typeof(IBookRepository),typeof(BookRepository));
            builder.Services.AddScoped(typeof(IGenerRepository), typeof(GenerRepository));
            builder.Services.AddScoped(typeof(IAuthorRepository), typeof(AuthorRepository));

            builder.Services.AddAutoMapper(m=>m.AddProfile(new BookProfile()));
            builder.Services.AddAutoMapper(m => m.AddProfile(new GenerProfile()));
            builder.Services.AddAutoMapper(m => m.AddProfile(new AuthorProfile()));


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
}
