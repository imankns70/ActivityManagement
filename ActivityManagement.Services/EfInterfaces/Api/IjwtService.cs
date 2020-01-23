using ActivityManagement.DomainClasses.Entities.Identity;
using System.Threading.Tasks;

namespace ActivityManagement.Services.Api.Contract
{
    public interface IjwtService
    {
        Task<string> GenerateTokenAsync(AppUser User);
    }
}
