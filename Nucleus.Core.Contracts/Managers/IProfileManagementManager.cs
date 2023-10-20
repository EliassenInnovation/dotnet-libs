using Nucleus.Core.Contracts.Models;
using System.Threading.Tasks;

namespace Nucleus.Core.Contracts.Managers
{
    public interface IProfileManagementManager
    {
        Task<ResponseModel<bool>> UpdateUserProfile(User user);
    }
}
