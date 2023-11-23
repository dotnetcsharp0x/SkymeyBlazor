using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Hosting;
using SkymeyBlazor.Components;
using SkymeyBlazor.Handlers;
using SkymeyBlazor.Interfaces.User;
using SkymeyBlazor.Model.Services;
using Toolbelt.Blazor;
using Toolbelt.Blazor.Extensions.DependencyInjection;


namespace SkymeyBlazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClientInterceptor();
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.Configuration["ServerUrl"])
            }.EnableIntercept(sp));
            builder.Services.AddSingleton<UserService>();

            builder.Services.AddBlazorBootstrap();
            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();
            SubscribeHttpClientInterceptorEvents(app);

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

            app.Run();
        }
        private static void SubscribeHttpClientInterceptorEvents(WebApplication host)
        {
            // Subscribe IHttpClientInterceptor's events.
            var httpInterceptor = host.Services.GetService<IHttpClientInterceptor>();
            httpInterceptor.BeforeSend += OnBeforeSend;
        }

        private static void OnBeforeSend(object sender, HttpClientInterceptorEventArgs args)
        {
            Console.WriteLine("BeforeSend event of HttpClientInterceptor");
            Console.WriteLine($"  - {args.Request.Method} {args.Request.RequestUri}");
        }

        private static async Task OnAfterSendAsync(object sender, HttpClientInterceptorEventArgs args)
        {
            Console.WriteLine("AfterSend event of HttpClientInterceptor");
            Console.WriteLine($"  - {args.Request.Method} {args.Request.RequestUri}");
            Console.WriteLine($"  - HTTP Status {args.Response?.StatusCode}");

            var capturedContent = await args.GetCapturedContentAsync();

            Console.WriteLine($"  - Content Headers");
            foreach (var headerText in capturedContent.Headers.Select(h => $"{h.Key}: {string.Join(", ", h.Value)}"))
            {
                Console.WriteLine($"    - {headerText}");
            }

            var httpContentString = await capturedContent.ReadAsStringAsync();
            Console.WriteLine($"  - HTTP Content \"{httpContentString}\"");
        }
    }
}
