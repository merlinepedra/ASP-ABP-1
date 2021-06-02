using Microsoft.EntityFrameworkCore;
using MyCompanyName.MyProjectName.Users;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore.Samples
{
    /* This is just an example test class.
     * Normally, you don't test ABP framework code
     * (like default AppUser repository IRepository<AppUser, Guid> here).
     * Only test your custom repository methods.
     */
    [Collection(MyProjectNameTestConsts.CollectionDefinitionName)]
    public class SampleRepositoryTests : MyProjectNameEntityFrameworkCoreTestBase
    {
        private readonly IRepository<AppUser, Guid> _appUserRepository;
        private readonly IRepository<MyEntity, Guid> _myEntityRepository;

        public SampleRepositoryTests()
        {
            _appUserRepository = GetRequiredService<IRepository<AppUser, Guid>>();
            _myEntityRepository = GetRequiredService<IRepository<MyEntity, Guid>>();
        }

        [Fact]
        public async Task Should_Query_AppUser()
        {
            /* Need to manually start Unit Of Work because
             * FirstOrDefaultAsync should be executed while db connection / context is available.
             */
            await WithUnitOfWorkAsync(async () =>
            {
                //Act
                var adminUser = await (await _appUserRepository.GetQueryableAsync())
                    .Where(u => u.UserName == "admin")
                    .FirstOrDefaultAsync();

                //Assert
                adminUser.ShouldNotBeNull();
            });

            await _myEntityRepository.InsertAsync(new MyEntity(Guid.NewGuid())
            {
                Price = 200.99M
            });

            await WithUnitOfWorkAsync(async () =>
            {
                //Act
                var entity = await (await _myEntityRepository.GetQueryableAsync()).OrderBy(x => x.Price)
                    .FirstOrDefaultAsync();

                //Assert
                entity.ShouldNotBeNull();

                entity.Price.ShouldBe(200.99m);
            });
        }
    }
}
