using ChatApp.Data;
using ChatApp.Hubs;
using ChatApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSession(op =>
            {
                op.IdleTimeout = TimeSpan.FromMinutes(20);
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add SignalR
            builder.Services.AddSignalR();
            var constr = builder.Configuration.GetConnectionString("Default")
                ?? throw new InvalidOperationException("No Connection String");

            builder.Services.AddSignalR();
            builder.Services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(constr);
            });

            builder.Services.AddIdentity<User, IdentityRole>
            (
                    option =>
                    {
                        option.Password.RequiredLength = 8;
                        option.Password.RequireUppercase = false;
                        option.Password.RequireLowercase = false;
                        option.Password.RequireNonAlphanumeric = false;
                    }
            ).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

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
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Map SignalR hub
            app.MapHub<ChatHub>("/chatHub");

            app.Run();
        }
    }
}
