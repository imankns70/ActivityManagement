using ActivityManagement.DataLayer.Context;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.Services.EfInterfaces.Api;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityManagement.Services.EfServices.Api
{
    class RefreshTokenService : IRefreshTokenService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<RefreshToken> _refreshTokens;
        public RefreshTokenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _refreshTokens = _unitOfWork.Set<RefreshToken>();
        }

        public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            _refreshTokens.Add(refreshToken);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<RefreshToken>> GetAllRefreshTokenByUserIdAsync(int userId)
        {
            return await _refreshTokens.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task RemoveAllRefreshTokenAsync(List<RefreshToken> refreshTokens)
        {
            _refreshTokens.RemoveRange(refreshTokens);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
