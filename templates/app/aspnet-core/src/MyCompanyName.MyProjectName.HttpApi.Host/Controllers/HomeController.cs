using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Authentication;

namespace MyCompanyName.MyProjectName.Controllers;

public class HomeController : ChallengeAccountController
{
    public ActionResult Index()
    {
        return Redirect("~/swagger");
    }
}
