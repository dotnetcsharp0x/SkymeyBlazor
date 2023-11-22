using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authentication;
using SkymeyBlazor.Components;
using SkymeyBlazor.Handlers;
using SkymeyBlazor.Interfaces.User;
using SkymeyBlazor.Model.Services;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace SkymeyBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddTransient<AuthenticationHandler>();

            builder.Services.AddHttpClient("ServerApi")
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ServerUrl"] ?? ""))
                .AddHttpMessageHandler<AuthenticationHandler>();

            builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
            builder.Services.AddBlazoredSessionStorageAsSingleton();
            builder.Services.AddBlazorBootstrap();
            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            await builder.Build().RunAsync();
        }
    }
}
