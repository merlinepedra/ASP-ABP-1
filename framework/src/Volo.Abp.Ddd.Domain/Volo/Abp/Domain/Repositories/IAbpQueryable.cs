using System.Linq;

namespace Volo.Abp.Domain.Repositories
{
    public interface IAbpQueryable<T> : IQueryable<T>
    {
        IAbpQueryable<T> Wrap(IQueryable<T> queryable);
    }
}
