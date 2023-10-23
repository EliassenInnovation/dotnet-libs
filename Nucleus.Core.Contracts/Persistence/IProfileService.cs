using Nucleus.Core.Contracts.Models;
using System;
using System.Threading.Tasks;

namespace Nucleus.Core.Contracts.Persistence
{
    public interface IProfileService
    {
        Task<ResponseModel<bool>> UpdateUserProfile(User user);

        Task<ResponseModel<User>> GetUserProfile(string userName);
    }
}
