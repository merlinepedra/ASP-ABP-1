using Microsoft.EntityFrameworkCore;
using MyCompanyName.MyProjectName.Users;
using Volo.Abp.EntityFrameworkCore;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore
{
    public interface IMyProjectNameDbContext : IEfCoreDbContext
    {
        DbSet<AppUser> Users { get; set; }

        DbSet<MyEntity> MyEntities { get; set; }
    }
}
