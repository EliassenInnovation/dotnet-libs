﻿using Microsoft.AspNetCore.Mvc;
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

        public async Task<bool> UpdateUserProfile(User user)
        {
           return await _profileService.UpdateUserProfile(user);
        }
    }
}
