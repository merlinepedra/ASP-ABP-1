using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace MyCompanyName.MyProjectName.BlazorTestApp.Client
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static async Task Main( string[] args )
        {
            //var builder = WebAssemblyHostBuilder.CreateDefault(args);

            //var application = builder.AddApplication<BlazorTestAppModule>(options =>
            //{
            //    options.UseAutofac();
            //});

            //var host = builder.Build();

            //await application.InitializeAsync(host.Services);

            //await host.RunAsync();

            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services
                .AddBlazorise(options =>
                {
                    options.ChangeTextOnKeyPress = true;
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            builder.Services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            builder.RootComponents.Add<Index>("#ApplicationContainer");

            var host = builder.Build();

            await host.RunAsync();
        }
    }
}
