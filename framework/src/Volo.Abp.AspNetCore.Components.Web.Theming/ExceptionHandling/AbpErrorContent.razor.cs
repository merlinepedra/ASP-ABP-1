using System;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;

namespace Volo.Abp.AspNetCore.Components.Web.Theming.ExceptionHandling;

public partial class AbpErrorContent
{
    [Parameter] 
    public Exception CurrentException { get; set; }

    [Parameter] 
    public EventCallback Refresh { get; set; }

    [Parameter] 
    public EventCallback Recover { get; set; }

    [Parameter]
    public EventCallback Close { get; set; }

    protected bool IsModalVisible { get; set; }

    protected virtual Task ShowModal()
    {
        IsModalVisible = true;
        return Task.CompletedTask;
    }

    protected virtual Task HideModal()
    {
        IsModalVisible = false;
        return Task.CompletedTask;
    }

    protected override async Task OnInitializedAsync()
    {
        await ShowModal();
    }

    protected virtual async Task OnRefreshAsync()
    {
        await Refresh.InvokeAsync();
    }

    protected virtual async Task OnRecoverAsync()
    {
        await HideModal();
        await Recover.InvokeAsync();
    }

    private Task OnModalClosing(ModalClosingEventArgs e)
    {
        e.Cancel = e.CloseReason != CloseReason.UserClosing;
        return Task.CompletedTask;
    }
}