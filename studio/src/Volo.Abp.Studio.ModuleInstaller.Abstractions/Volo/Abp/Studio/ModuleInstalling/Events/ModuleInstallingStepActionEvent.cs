using System;

namespace Volo.Abp.Studio.ModuleInstalling.Events;

public class ModuleInstallingStepActionEvent
{
    public Type ModuleInstallingStep { get; set; }

    public int CurrentStepNumber { get; set; }

    public int TotalStepCount { get; set; }

    public ModuleInstallingStepAction Action { get; set; }
}
