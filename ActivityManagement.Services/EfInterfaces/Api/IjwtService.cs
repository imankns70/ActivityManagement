using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.ViewModels.Api.RefreshToken;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ActivityManagement.Services.EfInterfaces.Api
{
    public interface IjwtService
    {
        Task<string> GenerateAccessTokenAsync(AppUser user);

        Task<ResponseTokenViewModel> AuthenticateUser(HttpRequest request, RequestTokenViewModel requestToken);
    }
}
