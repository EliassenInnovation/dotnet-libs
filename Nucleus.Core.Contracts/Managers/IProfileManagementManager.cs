using Nucleus.Core.Contracts.Models;
using System.Threading.Tasks;

namespace Nucleus.Core.Contracts.Managers
{
    public interface IProfileManagementManager
    {
        Task<bool> UpdateUserProfile(User user);
    }
}
