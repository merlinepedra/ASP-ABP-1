using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using DevHostServerProgram = MyCompanyName.MyProjectName.BlazorTestApp.Server.Program;

namespace MyCompanyName.MyProjectName.BlazorE2ETests.Infrastructure.ServerFixtures
{
    public class DevHostServerFixture<TProgram> : WebHostServerFixture
    {
        public string Environment { get; set; }
        public string PathBase { get; set; }
        public string ContentRoot { get; private set; }

        protected override IWebHost CreateWebHost()
        {
            ContentRoot = FindSampleOrTestSitePath( typeof( TProgram ).Assembly.GetName().Name );

            var args = new List<string>
            {
                "--urls", "http://127.0.0.1:0",
                "--contentroot", ContentRoot,
                "--pathbase", PathBase
            };

            if ( !string.IsNullOrEmpty( Environment ) )
            {
                args.Add( "--environment" );
                args.Add( Environment );
            }

            return DevHostServerProgram.BuildWebHost( args.ToArray() );
        }
    }
}
