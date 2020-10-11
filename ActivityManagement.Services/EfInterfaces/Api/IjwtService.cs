using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.ViewModels.Api.RefreshToken;
using System.Threading.Tasks;

namespace ActivityManagement.Services.EfInterfaces.Api
{
    public interface IjwtService
    {
        Task<string> GenerateAccessTokenAsync(AppUser user);

        Task<ResponseTokenViewModel> GenerateAccessAndRefreshToken(RequestTokenViewModel requestToken);
    }
}
