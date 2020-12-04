using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCompanyName.MyProjectName.Data;
using Serilog;
using Volo.Abp;

namespace MyCompanyName.MyProjectName.DbMigrator
{
    public class DbMigratorHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IServiceProvider _serviceProvider;

        public DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime, IServiceProvider serviceProvider)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var application = AbpApplicationFactory.Create<MyProjectNameDbMigratorModule>(options =>
            {
                options.UseAutofac();
                options.Services.AddLogging(c => c.AddSerilog());
                options.Configuration.EnvironmentName = _serviceProvider.GetRequiredService<IHostEnvironment>().EnvironmentName;
                options.Configuration.UserSecretsAssembly = typeof(DbMigratorHostedService).Assembly;
            }))
            {
                application.Initialize();

                await application
                    .ServiceProvider
                    .GetRequiredService<MyProjectNameDbMigrationService>()
                    .MigrateAsync();

                application.Shutdown();

                _hostApplicationLifetime.StopApplication();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
