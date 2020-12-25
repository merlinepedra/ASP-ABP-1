using System;
using System.Threading.Tasks;

namespace Volo.Abp.EntityFrameworkCore
{
    public interface IDbContextProvider<TDbContext>
        where TDbContext : IEfCoreDbContext
    {
        [Obsolete("Use GetDbContextAsync method.")]
        TDbContext GetDbContext();

        [Obsolete("Use GetInitializedAsync method.")]
        Task<TDbContext> GetDbContextAsync();

        TDbContext Get();

        Task<TDbContext> GetInitializedAsync();

        Task EnsureInitializedAsync(TDbContext dbContext);
    }
}
