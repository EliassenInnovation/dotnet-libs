using Nucleus.Core.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Core.Contracts.Persistence
{
    public interface IProfileService
    {
        //Return a success response
        Task UpdateUserProfile(User user);
    }
}
