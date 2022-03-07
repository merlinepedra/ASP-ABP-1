using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Gdpr;

namespace Volo.Abp.Identity.Gdpr;

public class IdentityUserGdprDataProvider : IGdprDataProvider, ITransientDependency
{
    protected IIdentityUserRepository IdentityUserRepository { get; }
    protected IdentityUserManager IdentityUserManager { get; }

    public IdentityUserGdprDataProvider(IIdentityUserRepository identityUserRepository, IdentityUserManager identityUserManager)
    {
        IdentityUserRepository = identityUserRepository;
        IdentityUserManager = identityUserManager;
    }
    
    public async Task<List<GdprFile>> GetFileAsync(Guid userId)
    {
        var user = await IdentityUserManager.GetByIdAsync(userId);

        var personalData = new Dictionary<string, string>();
        var personalDataProps = typeof(IdentityUser).GetProperties()
            .Where(prop => Attribute.IsDefined(prop, typeof(GdprDataAttribute)));

        foreach (var personalDataProp in personalDataProps)
        {
            personalData.Add(personalDataProp.Name, personalDataProp.GetValue(user)?.ToString() ?? string.Empty);
        }

        var userLogins = await IdentityUserManager.GetLoginsAsync(user);
        foreach (var userLogin in userLogins)
        {
            personalData.Add($"{userLogin.LoginProvider} external login provider key", userLogin.ProviderKey);
        }
        
        personalData.Add("Authenticator Key", await IdentityUserManager.GetAuthenticatorKeyAsync(user));
        
        var files = new List<GdprFile> 
        {
            new GdprFile("Personal Data", "application/json", personalData)
        };

        return files;
    }
}