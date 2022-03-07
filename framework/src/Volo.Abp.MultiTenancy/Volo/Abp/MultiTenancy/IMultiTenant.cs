using System;
using Volo.Abp.Gdpr;

namespace Volo.Abp.MultiTenancy;

public interface IMultiTenant
{
    /// <summary>
    /// Id of the related tenant.
    /// </summary>
    [GdprData]
    Guid? TenantId { get; }
}
