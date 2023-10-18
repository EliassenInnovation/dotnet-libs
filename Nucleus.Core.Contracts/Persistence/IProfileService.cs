using Nucleus.Core.Contracts.Models;
using System;
using System.Threading.Tasks;

namespace Nucleus.Core.Contracts.Persistence
{
    public interface IProfileService
    {
        Task<bool> UpdateUserProfile(User user);
    }
}
