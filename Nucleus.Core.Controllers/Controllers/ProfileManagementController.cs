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
            var response = await _profileManagementManager.UpdateUserProfile(user);
            return Ok(response);
        }
    }
}
