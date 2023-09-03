using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmployeeMVC.Data;
using EmployeeMVC.Areas.Identity.Data;
using EmployeeMVC.RabbitMQ;

namespace EmployeeMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("EmployeeMVCContextConnection")
                ?? throw new InvalidOperationException("Connection string 'EmployeeMVCContextConnection' not found.");

            builder.Services.AddDbContext<EmployeeMVCContext>(options => options.UseNpgsql(connectionString));
            builder.Services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();
            builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<EmployeeMVCContext>();

            //builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            //   .AddDefaultUI().AddDefaultTokenProviders()
            //   .AddEntityFrameworkStores<EmployeeMVCContext>();

            builder.Services.AddAuthentication().AddGoogle(opt =>
            {
                opt.ClientId = builder.Configuration["web:client_id"];
                opt.ClientSecret = builder.Configuration["web:client_secret"];
            });

            //builder.Services.AddAuthentication().AddFacebook(opt =>
            //{
            //    opt.AppId = builder.Configuration["facebook:app_id"];
            //    opt.AppSecret = builder.Configuration["facebook:app_secret"];
            //});


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;

            });
            var app = builder.Build();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseDirectoryBrowser("/pages");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();
            app.UseCors(opt =>
            {
                opt.WithMethods("GET","POST");
                opt.WithOrigins("https://www.microsoft.com", "");
                opt.WithHeaders("MyHeader");
            });
            app.UseAuthorization();
            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}