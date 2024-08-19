using ITI_MVC.Interfaces;
using ITI_MVC.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ITI_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IStudentInterface, StudentRepo>();
            builder.Services.AddScoped<IDepartmentInterface, DepartmentRepo>();
            builder.Services.AddScoped<IUser, UserRepo>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(c =>
            {
                c.LoginPath = "/account/login";
            });
            builder.Services.AddSession(s =>
            {
                //s.Cookie.Expiration = TimeSpan.FromMinutes(1);
            });
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("m1: Welcome M1\n");
            //    await next();
            //    await context.Response.WriteAsync("m1: Welcome after return from M2\n");
            //});

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("M2: Welcome in M2\n");
            //});

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // UseSession After Authorization
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
