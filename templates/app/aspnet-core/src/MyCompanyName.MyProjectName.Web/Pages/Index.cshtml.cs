using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Identity;

namespace MyCompanyName.MyProjectName.Web.Pages
{
    public class IndexModel : MyProjectNamePageModel
    {
        public IdentityUserManager UserManager { get; set; }

        public IndexModel(IdentityUserManager userManager)
        {
            UserManager = userManager;
        }

        public async Task OnGetAsync()
        {
            var token = "CfDJ8Mzn2ZAZ0CRLlLdwFqU3zbOdhDSJl31RnZZ8sGiOPveHlOi0by6sjE55Tb/p0axfWd1eg6j5o2JY/44eaSudtlTNfw857+nsTOXsSu8s/Fs5MlcDpO2MGwxlf3cGL4XlXDHcIOQfkgFzwFyDELexhrLk/vYRrNSI4QOST1xiZAna06t3/hxdWBalRxt99ncoPDqWd/keevO6hbaTbEbHEEPecvZy10t9JirWMT6XXJKyUTjSAuuttwCBsNKaGY4xww==";
            var admin = await UserManager.FindByNameAsync("admin");
            Console.WriteLine(await UserManager.VerifyUserTokenAsync(admin, UserManager.Options.Tokens.ChangeEmailTokenProvider, "EmailConfirmation", token));
        }
    }
}
