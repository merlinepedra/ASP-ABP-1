using Microsoft.EntityFrameworkCore;
using MyCompanyName.MyProjectName.Users;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore
{
    public class MyProjectNameDbContextInUnitTest : AbpDbContext<MyProjectNameDbContextInUnitTest>, IMyProjectNameDbContext
    {
        public DbSet<AppUser> Users { get; set; }

        public DbSet<MyEntity> MyEntities { get; set; }

        public MyProjectNameDbContextInUnitTest(DbContextOptions<MyProjectNameDbContextInUnitTest> options)
            : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            builder.Entity<AppUser>(b =>
            {
                b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser

                b.ConfigureByConvention();
                b.ConfigureAbpUser();

                /* Configure mappings for your additional properties
                 * Also see the MyProjectNameEfCoreEntityExtensionMappings class
                 */
            });

            /* Configure your own tables/entities inside the ConfigureMyProjectName method */

            builder.ConfigureMyProjectName();

            builder.Entity<MyEntity>()
                .Property(e => e.Price)
                .HasConversion<double>();
        }
    }
}
