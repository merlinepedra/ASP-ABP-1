using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp.Linq;

namespace Volo.Abp.Domain.Repositories
{
    public abstract class AbpQueryableWrapper<T> : IAbpQueryable<T>, ILazyInitializedQueryProvider<T>
    {
        protected readonly IQueryable<T> Queryable;
        protected  readonly IAbpQueryable<T> AbpQueryable;
        protected readonly ILazyInitializedQueryProvider<T> LazyProvider;

        protected AbpQueryableWrapper(
            IQueryable<T> queryable,
            IAbpQueryable<T> abpQueryable)
        {
            Queryable = queryable;
            AbpQueryable = abpQueryable;
            LazyProvider = abpQueryable.AsLazyInitializedQueryProvider();
        }

        public IAbpQueryable<T> Wrap(IQueryable<T> queryable)
        {
            return AbpQueryable.Wrap(queryable);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Queryable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType => Queryable.ElementType;
        public Expression Expression => Queryable.Expression;
        public IQueryProvider Provider => Queryable.Provider;

        public IAsyncQueryableExecuter AsyncExecuter => LazyProvider.AsyncExecuter;
        public Task EnsureInitializedAsync()
        {
            return LazyProvider.EnsureInitializedAsync();
        }

        public IQueryable<T> GetQueryable()
        {
            return AbpQueryable;
        }
    }
}
