using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using pruebatecnicapeaku.Client.Repositories;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using pruebatecnicapeaku.Client.Auth;

namespace pruebatecnicapeaku.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
           
            services.AddScoped<IRepository, Repository>();
            services.AddBlazoredToast();
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationProviderJWT>();

            services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(
                provider=>provider.GetRequiredService<AuthenticationProviderJWT>());

            services.AddScoped<ILoginService, AuthenticationProviderJWT>(
                provider => provider.GetRequiredService<AuthenticationProviderJWT>());
        }
    }
}
