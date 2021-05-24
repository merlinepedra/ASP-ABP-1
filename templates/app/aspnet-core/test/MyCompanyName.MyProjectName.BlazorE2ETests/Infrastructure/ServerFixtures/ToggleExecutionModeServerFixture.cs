using System;

namespace MyCompanyName.MyProjectName.BlazorE2ETests.Infrastructure.ServerFixtures
{
    public class ToggleExecutionModeServerFixture<TClientProgram>
        : ServerFixture
    {
        public string PathBase { get; set; }
        public bool UsingAspNetHost { get; private set; }

        private AspNetSiteServerFixture.BuildWebHost _buildWebHostMethod;
        private IDisposable _serverToDispose;

        public void UseAspNetHost(AspNetSiteServerFixture.BuildWebHost buildWebHostMethod)
        {
            _buildWebHostMethod = buildWebHostMethod
                ?? throw new ArgumentNullException(nameof(buildWebHostMethod));
            UsingAspNetHost = true;
        }

        protected override string StartAndGetRootUri()
        {
            if (_buildWebHostMethod == null)
            {
                // Use Blazor's dev host server
                var underlying = new DevHostServerFixture<TClientProgram>();
                underlying.PathBase = PathBase;
                _serverToDispose = underlying;
                return underlying.RootUri.AbsoluteUri;
            }
            else
            {
                // Use specified ASP.NET host server
                var underlying = new AspNetSiteServerFixture();
                underlying.BuildWebHostMethod = _buildWebHostMethod;
                _serverToDispose = underlying;
                return underlying.RootUri.AbsoluteUri;
            }
        }

        public override void Dispose()
        {
            _serverToDispose?.Dispose();
        }
    }
}
