using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Volo.Abp.Domain.Repositories.EntityFrameworkCore
{
    public class EfCoreAbpQueryableWrapper<T> : AbpQueryableWrapper<T>, IAsyncEnumerable<T>
    {
        public EfCoreAbpQueryableWrapper(
            IQueryable<T> queryable,
            IAbpQueryable<T> abpQueryable
            ) : base(queryable, abpQueryable)
        {

        }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            return AbpQueryable.AsAsyncEnumerable().GetAsyncEnumerator(cancellationToken);
        }
    }
}
