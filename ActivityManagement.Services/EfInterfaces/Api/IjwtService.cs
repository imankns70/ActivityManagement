using ActivityManagement.DomainClasses.Entities.Identity;
using System.Threading.Tasks;

namespace ActivityManagement.Services.EfInterfaces.Api
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(AppUser user);
    }
}
