using System;
using System.Threading.Tasks;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Security.Claims;
using Volo.Abp.Users;

namespace MyCompanyName.MyProjectName;

public class MyHangfireAuthorizationFilter : IDashboardAsyncAuthorizationFilter
{
    private readonly bool _enableTenant;
    private readonly string _requiredPermissionName;

    public MyHangfireAuthorizationFilter(bool enableTenant = false, string requiredPermissionName = null)
    {
        _enableTenant = requiredPermissionName.IsNullOrWhiteSpace() ? enableTenant : true;
        _requiredPermissionName = requiredPermissionName;
    }

    public async Task<bool> AuthorizeAsync(DashboardContext context)
    {
        if (!await IsLoggedInAsync(context, _enableTenant))
        {
            return false;
        }

        if (_requiredPermissionName.IsNullOrEmpty())
        {
            return true;
        }

        return await IsPermissionGrantedAsync(context, _requiredPermissionName);
    }

    private async static Task<bool> IsLoggedInAsync(DashboardContext context, bool enableTenant)
    {
        var authenticateResult = await context.GetHttpContext().AuthenticateAsync("HangfireCookies");

        if (authenticateResult.Succeeded)
        {
            using (context.GetHttpContext().RequestServices.GetRequiredService<ICurrentPrincipalAccessor>().Change(authenticateResult.Principal))
            {
                var currentUser = context.GetHttpContext().RequestServices.GetRequiredService<ICurrentUser>();

                if (!enableTenant)
                {
                    return currentUser.IsAuthenticated && !currentUser.TenantId.HasValue;
                }

                return currentUser.IsAuthenticated;
            }

        }

        return false;
    }

    private async static Task<bool> IsPermissionGrantedAsync(DashboardContext context, string requiredPermissionName)
    {
        var permissionChecker = context.GetHttpContext().RequestServices.GetRequiredService<IPermissionChecker>();
        return await permissionChecker.IsGrantedAsync(requiredPermissionName);
    }
}
