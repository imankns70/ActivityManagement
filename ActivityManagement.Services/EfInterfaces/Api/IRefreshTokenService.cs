using ActivityManagement.DomainClasses.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActivityManagement.Services.EfInterfaces.Api
{
   public interface IRefreshTokenService
    {
        Task AddRefreshTokenAsync(RefreshToken refreshToken);
        Task RemoveAllRefreshTokenAsync(List<RefreshToken> refreshTokens);
        Task<List<RefreshToken>> GetAllRefreshTokenByUserIdAsync(int userId);
    }
}
