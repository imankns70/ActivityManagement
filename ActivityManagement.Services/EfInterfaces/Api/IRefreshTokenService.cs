using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.ViewModels.Api.RefreshToken;
using ActivityManagement.ViewModels.SiteSettings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActivityManagement.Services.EfInterfaces.Api
{
    public interface IRefreshTokenService
    {
        RefreshToken CreateRefreshToken(RefreshTokenSetting refreshTokenSetting, int userId, bool IsRemember);
        Task AddRefreshTokenAsync(RefreshToken refreshToken);
        Task RemoveAllRefreshTokenAsync(List<RefreshToken> refreshTokens);
        Task<List<RefreshToken>> GetAllRefreshTokenByUserIdAsync(int userId);

        Task<RefreshToken> OldRefreshToken(string clientId, string refreshToken);
    }
}
