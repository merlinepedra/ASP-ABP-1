using Volo.Abp.Cli.ProjectBuilding.Building;

namespace Volo.Abp.Cli.ProjectBuilding.Templates.App;

public class AppNoLayersTemplate : AppNoLayersTemplateBase
{
    /// <summary>
    /// "app-nolayers".
    /// </summary>
    public const string TemplateName = "app-nolayers";

    public AppNoLayersTemplate()
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

        if (context.BuildArgs.Theme == Theme.LeptonXLite)
        {
            context.Symbols.Add("LEPTONX-LITE");
        }

        if (context.BuildArgs.Theme == Theme.Basic)
        {
            context.Symbols.Add("BASIC");
        }
    }
}
