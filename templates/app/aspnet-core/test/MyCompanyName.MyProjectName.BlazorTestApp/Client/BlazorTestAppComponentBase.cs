using MyCompanyName.MyProjectName.Localization;
using Volo.Abp.AspNetCore.Components;

namespace MyCompanyName.MyProjectName.BlazorTestApp.Client
{
    public abstract class BlazorTestAppComponentBase : AbpComponentBase
    {
        protected BlazorTestAppComponentBase()
        {
            LocalizationResource = typeof(MyProjectNameResource);
        }
    }
}
