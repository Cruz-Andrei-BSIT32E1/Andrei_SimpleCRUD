using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentSectionApp.Application.Interfaces;
using StudentSectionApp.Application.Services;
using StudentSectionApp.Domain.Interfaces;
using StudentSectionApp.Infrastructure.Repositories;

namespace StudentSectionApp.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<IStudentRepository, StudentRepository>(); // Register as Singleton
            builder.Services.AddScoped<ISectionRepository, SectionRepository>();
            builder.Services.AddScoped<IStudentService, StudentService>(); // Register the IStudentService
            builder.Services.AddScoped<ISectionService, SectionService>(); // Register the ISectionService
            builder.Services.AddHttpContextAccessor(); // Add this line to register IHttpContextAccessor

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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