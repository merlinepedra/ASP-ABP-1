using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp.BlazoriseUI;

namespace MyCompanyName.MyProjectName.BlazorTestApp.Client
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static async Task Main(string[] args)
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

            ConfigureBlazorise(builder.Services);
            ConfigureABP(builder.Services);

            builder.Services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            builder.RootComponents.Add<Index>("#ApplicationContainer");

            var host = builder.Build();

            await host.RunAsync();
        }

        private static void ConfigureBlazorise(IServiceCollection services)
        {
            services
                .AddBlazorise()
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();
        }

        private static void ConfigureABP(IServiceCollection services)
        {
            services.AddSingleton<BlazoriseUiMessageService>();
            services.AddSingleton(typeof(IStringLocalizer<>), typeof(DummyStringLocalizer<>));
        }

        class DummyStringLocalizer<T> : IStringLocalizer<T>
        {
            public LocalizedString this[string name] => null;

            public LocalizedString this[string name, params object[] arguments] => null;

            public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => null;
        }
    }
}
