﻿using ActivityManagement.DataLayer.Context;
using ActivityManagement.DomainClasses.Entities.Identity;
using ActivityManagement.Services.EfInterfaces.Api;
using ActivityManagement.ViewModels.Api.RefreshToken;
using ActivityManagement.ViewModels.SiteSettings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityManagement.Services.EfServices.Api
{
   public class RefreshTokenService : IRefreshTokenService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<RefreshToken> _refreshTokens;
        public RefreshTokenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _refreshTokens = _unitOfWork.Set<RefreshToken>();
        }

        public RefreshToken CreateRefreshToken(RefreshTokenSetting refreshTokenSetting, int userId, bool IsRemember)
        {

            
            return new RefreshToken
            {
                ClientId = refreshTokenSetting.ClientId,
                UserId = userId,
                Value = Guid.NewGuid().ToString("N"),
                ExpireDate = IsRemember ? DateTime.Now.AddDays(refreshTokenSetting.ExpireDate) : DateTime.Now.AddDays(1)
            };
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

        public async Task<RefreshToken> OldRefreshToken(string clientId, string refreshToken)
        {
           return await _refreshTokens.FirstOrDefaultAsync(x => x.ClientId == clientId && x.Value == refreshToken && x.ExpireDate > DateTime.Now );
 
        }

        public async Task RemoveAllRefreshTokenAsync(List<RefreshToken> refreshTokens)
        {
            _refreshTokens.RemoveRange(refreshTokens);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
