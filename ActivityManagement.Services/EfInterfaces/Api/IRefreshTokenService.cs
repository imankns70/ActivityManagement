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
        RefreshToken CreateRefreshToken(RefreshTokenSetting refreshTokenSetting, int userId, bool IsRemember, string ipAddress);
        Task AddRefreshTokenAsync(RefreshToken refreshToken);
        Task RemoveRefreshTokenAsync(RefreshToken refreshTokens);
        Task<RefreshToken> GetRefreshTokenByUserIdAsync(int userId);

        Task<RefreshToken> OldRefreshToken(string clientId, string refreshToken, string ipAddress);

        //Task<bool> CheckRefreshToken(DateTime expireDate, int userId);
    }
}
