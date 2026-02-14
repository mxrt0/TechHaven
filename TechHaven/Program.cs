using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechHaven.Common;
using TechHaven.Data;
using TechHaven.Data.Models;
using TechHaven.Data.Seeders;
using TechHaven.Services.Admin;
using TechHaven.Services.Contracts.Admin;
using TechHaven.Services.Contracts.Public;
using TechHaven.Services.Public;
// TODO: Polish UI
namespace TechHaven
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;           
                options.Password.RequireLowercase = false;      
                options.Password.RequireUppercase = false;       
                options.Password.RequireNonAlphanumeric = false; 
                options.Password.RequiredLength = 6;            
                options.Password.RequiredUniqueChars = 1;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = false;

            })
              .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddScoped<IWishlistService, WishlistService>();

            builder.Services.AddScoped<ICartService, CartService>();

            builder.Services.AddScoped<IOrderService, OrderService>();  
            builder.Services.AddScoped<IAdminOrderService, AdminOrderService>();

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IAdminProductService, AdminProductService>();

            builder.Services.AddScoped<ICategoryService, CategoryService>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                await AdminSeeder.SeedAdminRoleAsync(roleManager);
                await AdminSeeder.EnsureAdminUserAsync(userManager);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                var path = context.Request.Path.Value ?? "";

                if (path == "/" || path == "")
                {
                    if (context.User.Identity?.IsAuthenticated ?? false)
                    {
                        if (context.User.IsInRole("Admin"))
                        {
                            context.Response.Redirect("/Admin");
                            return;
                        }
                    }
                }

                await next();
            });
                                 
            app.MapStaticAssets();

            app.MapControllerRoute(
               name: "areas",
               pattern: "{area:required}/{controller=Home}/{action=Index}/{id?}")
               .WithStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
           

            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
