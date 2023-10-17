using Nucleus.Core.Contracts.Managers;
using Nucleus.Core.Contracts.Models;
using Nucleus.Core.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<ResponseModel<User>> UpdateUserProfile(User user)
        {
           //Going to return a success and not the model
           //return await _profileService.UpdateUserProfile(user);
        }
    }
}
