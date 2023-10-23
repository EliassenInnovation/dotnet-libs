using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nucleus.Core.Contracts.Managers;
using Nucleus.Core.Contracts.Models;
using System.Threading.Tasks;

namespace Nucleus.Core.Controllers.Controllers
{
    /// <summary>
    /// This controller is used for updating user profile information
    /// Either as an admin or the user themseleves 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileManagementController : ControllerBase
    {
        private readonly IProfileManagementManager _profileManagementManager;

        public ProfileManagementController(IProfileManagementManager profileManagementManager)
        {
            _profileManagementManager = profileManagementManager;
        }

        /// <summary>
        /// Update user profile details
        /// </summary>
        /// <returns> A true or false if the record was updated </returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateUserProfile(User user)
        {
            return Ok(await _profileManagementManager.UpdateUserProfile(user));
        }

        /// <summary>
        /// Get the updated user profile details
        /// </summary>        
        /// <param name="userName">This is the guid from b2c</param>
        /// <returns> The User Profile in its most recent state </returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ResponseModel<User>> GetUserProfile(string id)
        {
            return await _profileManagementManager.GetUserProfile(id);
        }

    }
}
