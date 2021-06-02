using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.Identity;

namespace MyCompanyName.MyProjectName.Pages
{
    public class IndexModel : AbpPageModel
    {
        public IdentityUserManager UserManager { get; set; }

        public IndexModel(IdentityUserManager userManager)
        {
            UserManager = userManager;
        }

        public async Task OnGetAsync()
        {

            var admin = await UserManager.FindByNameAsync("admin");
            var token = await UserManager.GenerateEmailConfirmationTokenAsync(admin);
            Console.WriteLine(token);
        }
    }
}
