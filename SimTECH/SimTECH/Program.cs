using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SimTECH.Data;
using SimTECH.Data.Requirements;
using SimTECH.Providers;
using SimTECH.Services;

namespace SimTECH
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure database context services
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContextFactory<SimTechDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.ConfigureWarnings(e => e.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning));
            });
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            // Add custom services
            builder.Services.AddScoped<SimAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(e => e.GetRequiredService<SimAuthenticationStateProvider>());
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<IAuthorizationHandler, CoolRequirementHandler>();
            builder.Services.AddScoped<IAuthorizationHandler, ChoiceRequirementHandler>();
            //builder.Services.AddSingleton<ExampleService>();

            // Add authorization policies
            builder.Services.AddAuthorizationCore(config =>
            {
                config.AddPolicy("CoolOnly", policy => policy.AddRequirements(new CoolRequirement()));
                config.AddPolicy("CoolAdminOnly", policy =>
                {
                    policy.AddRequirements(new CoolRequirement());
                    policy.RequireRole("admin");
                });
                config.AddPolicy("ChoicePolicy", policy => policy.AddRequirements(new ChoiceRequirement()));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}