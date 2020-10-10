using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.ViewModels.Api.RefreshToken;
using System.Threading.Tasks;

namespace ActivityManagement.Services.EfInterfaces.Api
{
    public interface IJwtService
    {
        Task<string> GenerateAccessTokenAsync(AppUser user);

        Task<string> GenerateRefreshToken(RequestTokenViewModel requestToken);
    }
}
