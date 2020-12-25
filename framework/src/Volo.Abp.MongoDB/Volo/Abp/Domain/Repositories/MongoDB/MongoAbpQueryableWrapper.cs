using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Volo.Abp.Domain.Repositories.MongoDB
{
    public class MongoAbpQueryableWrapper<T> : AbpQueryableWrapper<T>, IMongoQueryable<T>

    {
        public MongoAbpQueryableWrapper(
            IQueryable<T> queryable,
            IAbpQueryable<T> abpQueryable
            ) : base(queryable, abpQueryable)
        {

        }

        public QueryableExecutionModel GetExecutionModel()
        {
            throw new System.NotImplementedException();
        }

        public IAsyncCursor<T> ToCursor(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<IAsyncCursor<T>> ToCursorAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }
    }
}
