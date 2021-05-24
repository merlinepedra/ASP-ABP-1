using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace MyCompanyName.MyProjectName.BlazorTestApp.Client
{
    [Dependency(ReplaceServices = true)]
    public class BlazorTestAppBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "BlazorTestApp";
    }
}
