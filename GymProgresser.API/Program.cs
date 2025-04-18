using FluentValidation;
using GymProgresser.Application.Auth;
using GymProgresser.Application.Auth.Interfaces;
using GymProgresser.Application.Auth.Validators;
using GymProgresser.Application.Users;
using GymProgresser.Infrastructure.EF;
using GymProgresser.Infrastructure.Repositories;
using GymProgresser.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GymProgresser.API
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
            builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
            var assembly = Assembly.GetExecutingAssembly();
            builder.Services.AddDbContext<GymProgressDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IPasswordManager, PasswordManager>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
