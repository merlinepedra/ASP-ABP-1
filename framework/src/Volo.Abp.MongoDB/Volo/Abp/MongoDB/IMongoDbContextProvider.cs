using System;
using System.Threading;
using System.Threading.Tasks;

namespace Volo.Abp.MongoDB
{
    public interface IMongoDbContextProvider<TMongoDbContext>
        where TMongoDbContext : IAbpMongoDbContext
    {
        [Obsolete("Use CreateDbContextAsync")]
        TMongoDbContext GetDbContext();

        [Obsolete("Use GetInitializedAsync method.")]
        Task<TMongoDbContext> GetDbContextAsync(CancellationToken cancellationToken = default);

        TMongoDbContext Get();

        Task<TMongoDbContext> GetInitializedAsync(CancellationToken cancellationToken = default);

        Task EnsureInitializedAsync(TMongoDbContext dbContext, CancellationToken cancellationToken = default);
    }
}
