using Nucleus.Core.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Core.Contracts.Managers
{
    public interface IProfileManagementManager
    {
        Task<ResponseModel<User>> UpdateUserProfile(User user);
    }
}
