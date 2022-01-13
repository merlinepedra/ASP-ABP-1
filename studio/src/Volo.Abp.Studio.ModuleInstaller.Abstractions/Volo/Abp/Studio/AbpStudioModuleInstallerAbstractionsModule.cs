using Volo.Abp.EventBus;
using Volo.Abp.Modularity;
using Volo.Abp.Studio.Analyzing;

namespace Volo.Abp.Studio;

[DependsOn(
    typeof(AbpStudioAnalyzingAbstractionsModule),
    typeof(AbpEventBusModule)
)]
public class AbpStudioModuleInstallerAbstractionsModule : AbpModule
{

}
