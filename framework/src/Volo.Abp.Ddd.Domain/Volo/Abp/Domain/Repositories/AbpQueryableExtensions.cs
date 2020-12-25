using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Volo.Abp.Domain.Repositories
{
    public static class AbpQueryableExtensions
    {
        public static IAbpQueryable<T> Where<T>(this IAbpQueryable<T> abpQueryable, Expression<Func<T, bool>> predicate)
        {
            return abpQueryable.Wrap(
                abpQueryable
                    .AsLazyInitializedQueryProvider()
                    .GetQueryable()
                    .Where(predicate)
            );
        }

        public static async Task<List<T>> ToListAsync<T>(this IAbpQueryable<T> abpQueryable)
        {
            var lazyProvider = abpQueryable.AsLazyInitializedQueryProvider();
            await lazyProvider.EnsureInitializedAsync();
            return await lazyProvider.AsyncExecuter.ToListAsync(lazyProvider.GetQueryable());
        }

        internal static ILazyInitializedQueryProvider<T> AsLazyInitializedQueryProvider<T>(this IAbpQueryable<T> abpQueryable)
        {
            var lazyQueryProvider = abpQueryable as ILazyInitializedQueryProvider<T>;
            if (lazyQueryProvider == null)
            {
                throw new AbpException($"Given object doesn't support {typeof(ILazyInitializedQueryProvider<>).FullName}");
            }

            return lazyQueryProvider;
        }
    }
}
