using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EventBus.Local;
using Volo.Abp.Studio.ModuleInstalling.Events;

namespace Volo.Abp.Studio.ModuleInstalling;

public class ModuleInstallingPipeline
{
    public ModuleInstallingContext Context { get; }

    private ILocalEventBus LocalEventBus { get; }

    public List<ModuleInstallingPipelineStep> Steps { get; }

    public ModuleInstallingPipeline(ModuleInstallingContext context)
    {
        Context = context;
        LocalEventBus = context.StudioServiceProvider.GetRequiredService<ILocalEventBus>();
        Steps = new List<ModuleInstallingPipelineStep>();
    }

    public async Task ExecuteAsync()
    {
        for (var index = 0; index < Steps.Count; index++)
        {
            await PublishEventAsync(Steps[index].GetType(), ModuleInstallingStepAction.started, Steps.Count, index);

            try
            {
                await Steps[index].ExecuteAsync(Context);

                await PublishEventAsync(Steps[index].GetType(), ModuleInstallingStepAction.succeeded, Steps.Count, index);
            }
            catch (Exception)
            {
                await PublishEventAsync(Steps[index].GetType(), ModuleInstallingStepAction.failed, Steps.Count, index);
            }
        }
    }

    private async Task PublishEventAsync(Type type, ModuleInstallingStepAction action, int totalStepCount, int index)
    {
        await LocalEventBus.PublishAsync(new ModuleInstallingStepActionEvent {
            ModuleInstallingStep = type,
            Action = ModuleInstallingStepAction.started,
            TotalStepCount = totalStepCount,
            CurrentStepNumber = index
        });
    }

    public void Add(ModuleInstallingPipelineStep step)
    {
        Steps.Add(step);
    }

    public void Remove(Type stepType)
    {
        Steps.RemoveAll(step => step.GetType() == stepType);
    }

    public void Replace(Type stepType, ModuleInstallingPipelineStep step)
    {
        Steps.ReplaceOne(step => step.GetType() == stepType, step);
    }

    public void InsertAfter(Type stepType, ModuleInstallingPipelineStep step)
    {
        Steps.InsertAfter(step => step.GetType() == stepType, step);
    }

    public void InsertBefore(Type stepType, ModuleInstallingPipelineStep step)
    {
        Steps.InsertBefore(step => step.GetType() == stepType, step);
    }
}
