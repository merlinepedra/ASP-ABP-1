using Volo.Abp.Cli.ProjectBuilding.Building;

namespace Volo.Abp.Cli.ProjectBuilding.Templates.App;

public class AppNoLayersProTemplate : AppNoLayersTemplateBase
{
    /// <summary>
    /// "app-nolayers-pro".
    /// </summary>
    public const string TemplateName = "app-nolayers-pro";

    public AppNoLayersProTemplate()
        : base(TemplateName)
    {
        //TODO: Change URL
        //DocumentUrl = CliConsts.DocsLink + "/en/abp/latest/Startup-Templates/Application";
    }

    protected override void ConfigureTheme(ProjectBuildContext context)
    {
        if (!context.BuildArgs.Theme.HasValue)
        {
            return;
        }

        if (context.BuildArgs.Theme == Theme.LeptonX)
        {
            context.Symbols.Add("LEPTONX");
        }

        if (context.BuildArgs.Theme == Theme.Lepton)
        {
            context.Symbols.Add("LEPTON");
        }
    }
}
