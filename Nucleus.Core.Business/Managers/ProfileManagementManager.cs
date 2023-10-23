using Nucleus.Core.Contracts.Managers;
using Nucleus.Core.Contracts.Models;
using Nucleus.Core.Contracts.Persistence;
using System.Threading.Tasks;

namespace Nucleus.Core.Business.Managers
{
    public class ProfileManagementManager : IProfileManagementManager
    {
        private readonly IProfileService _profileService;

        public ProfileManagementManager(IProfileService profileService) 
        {
            _profileService = profileService;
        }

        public async Task<ResponseModel<bool>> UpdateUserProfile(User user)
        {
           return await _profileService.UpdateUserProfile(user);
        }

        public async Task<ResponseModel<User>> GetUserProfile(string userName)
        {
            return await _profileService.GetUserProfile(userName);
        }
    }
}
