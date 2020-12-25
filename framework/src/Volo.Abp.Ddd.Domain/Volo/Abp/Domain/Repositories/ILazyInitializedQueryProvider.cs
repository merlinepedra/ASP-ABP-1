using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Linq;

namespace Volo.Abp.Domain.Repositories
{
    public interface ILazyInitializedQueryProvider<T>
    {
        IAsyncQueryableExecuter AsyncExecuter { get; }

        Task EnsureInitializedAsync();

        IQueryable<T> GetQueryable();
    }
}
