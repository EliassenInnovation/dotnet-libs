using Eliassen.Microsoft.B2C.Identity;
using Nucleus.Core.Contracts.Managers;
using Nucleus.Core.Contracts.Models;
using Nucleus.Core.Contracts.Models.Keys;
using Nucleus.Core.Contracts.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Nucleus.Core.Business.Managers
{
    public class UserManagementManager : IUserManagementManager
    {
        private readonly IIdentityManager _identityManager;
        private readonly IUserService _users;
        private readonly IModuleService _modules;

        public UserManagementManager(
            IIdentityManager identityManager,
            IUserService users,
            IModuleService modules
            )
        {
            _identityManager = identityManager;
            _users = users;
            _modules = modules;
        }

        public IQueryable<User> QueryUsers() => _users.Query();


        public IQueryable<Module> QueryModules() => _modules.Query();

        public async Task<ResponseModel<User>> SaveUserAsync(UserAction? user)
        {

            if (user == null || string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName) || string.IsNullOrEmpty(user.EmailAddress))
                return new ResponseModel<User>()
                {
                    IsSuccess = false,
                    Message = "Missing Required Fields"
                };

            if (user.IdentityAction == UserActionsConst.IdentityActions.RetainIdentity && user.UserName == null)
            {
                // Need to add/assign an account to this user
                var (newId, _) = await _identityManager.CreateIdentityUserAsync(user.EmailAddress, user.FirstName, user.LastName);
                user.UserName = newId;
            }

            if ((user.IdentityAction == UserActionsConst.IdentityActions.RemoveIdentity
                || user.IdentityAction == UserActionsConst.IdentityActions.RemoveAccount)
                && user.UserName != null)
            {
                bool removed = await _identityManager.RemoveIdentityUserAsync(user.UserName);
                if (removed == true)
                    user.UserName = null;
            }

            ResponseModel<User?> result = new ResponseModel<User?>();
            if (user.IdentityAction == UserActionsConst.IdentityActions.RemoveAccount)
            {
                await _users.RemoveAsync(user.UserId ?? throw new NotSupportedException($"{nameof(user.UserId)}: {user.UserId}"));
            }
            else if (string.IsNullOrEmpty(user.UserId))
            {
                var userExists = await _users.GetByEmailAddressAsync(user.EmailAddress);
                if (userExists != null)
                {
                    return new ResponseModel<User>()
                    {
                        IsSuccess = false,
                        Message = "Duplicate Email Address"
                    };
                }
                else
                {
                    user.CreatedOn = DateTimeOffset.Now;
                    await _users.CreateAsync(user);
                    result.Response = await _users.GetByEmailAddressAsync(user.EmailAddress);
                }
            }
            else
            {
                await _users.UpdateAsync(user);
                result.Response = await _users.GetByUserIdAsync(user.UserId);
            }

            return new ResponseModel<User>()
            {
                IsSuccess = true,
                Response = user
            };
        }
    }
}
