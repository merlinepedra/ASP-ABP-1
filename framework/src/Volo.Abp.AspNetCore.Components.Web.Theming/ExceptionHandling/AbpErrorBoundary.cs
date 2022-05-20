using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace Volo.Abp.AspNetCore.Components.Web.Theming.ExceptionHandling;

public class AbpErrorBoundary : ErrorBoundaryBase
{
    [Inject] 
    protected IErrorBoundaryLogger? ErrorBoundaryLogger { get; set; }

    [Inject] 
    protected NavigationManager NavigationManager { get; set; }

    protected override async Task OnErrorAsync(Exception exception)
    {
        if (!IsPageError(exception))
        {
            // Ignore it
            return;
        }

        await ErrorBoundaryLogger!.LogErrorAsync(exception);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (CurrentException is null)
        {
            builder.AddContent(0, ChildContent);
        }
        else if (!IsPageError(CurrentException))
        {
            // Ignore it
            return;
        }
        else if (ErrorContent is not null)
        {
            builder.AddContent(1, ErrorContent(CurrentException));
        }
        else
        {
            builder.OpenComponent<AbpErrorContent>(2);
            builder.AddAttribute(3, "CurrentException", CurrentException);
            builder.AddAttribute(4, "Refresh", EventCallback.Factory.Create(this, RefreshAsync));
            builder.AddAttribute(5, "Recover", EventCallback.Factory.Create(this, RecoverAsync));
            builder.CloseElement();
        }
    }

    protected virtual Task RefreshAsync() // We can take fallback uri as parameter
    {
        NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
        return Task.CompletedTask;
    }

    protected new virtual Task RecoverAsync()
    {
        base.Recover();
        return Task.CompletedTask;
    }

    protected virtual bool IsPageError(Exception exception)
    {
        return !exception.StackTrace.IsNullOrEmpty() &&
               exception.StackTrace!.Contains("BuildRenderTree(RenderTreeBuilder __builder)");
    }
}