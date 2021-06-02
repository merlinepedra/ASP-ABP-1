using Microsoft.EntityFrameworkCore;
using MyCompanyName.MyProjectName.Users;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore
{
    public static class MyProjectNameDbContextModelCreatingExtensions
    {
        public static void ConfigureMyProjectName(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(MyProjectNameConsts.DbTablePrefix + "YourEntities", MyProjectNameConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});

            builder.Entity<MyEntity>(b =>
            {
                b.ToTable(MyProjectNameConsts.DbTablePrefix + "MyEntities", MyProjectNameConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
            });
        }
    }
}
