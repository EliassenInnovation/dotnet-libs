using MongoDB.Driver;
using Nucleus.Core.Contracts.Models;
using Nucleus.Core.Contracts.Persistence;
using Nucleus.Core.Persistence.Collections;
using Nucleus.Core.Shared.Persistence.Services.ServiceHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Core.Persistence.Services
{
    public class ProfileService : IProfileService
    {
        private readonly ICoreMongoDatabase _db;
        private readonly BsonCollectionBuilder<User, UserCollection> _userCollectionBuilder;

        public ProfileService(ICoreMongoDatabase db)
        {
            _db = db;
            _userCollectionBuilder = new BsonCollectionBuilder<User, UserCollection>();
        }

        public async Task<ResponseModel<bool>> UpdateUserProfile(User user)
        {
            var filter = Builders<UserCollection>.Filter.Eq(u => u.UserId, user.UserId);
            var update = Builders<UserCollection>.Update
                .Set(u => u.FirstName, user.FirstName)
                .Set(u => u.LastName, user.LastName)
                .Set(u => u.Active, user.Active)
                .Set(u => u.AddressLine1, user.AddressLine1)
                .Set(u => u.AddressLine2, user.AddressLine2)
                .Set(u => u.Bio, user.Bio)
                .Set(u => u.City, user.City)
                .Set(u => u.Country, user.Country)
                .Set(u => u.StateProvince, user.StateProvince)
                .Set(u => u.PhoneNumber, user.PhoneNumber)
                .Set(u => u.NotificationSettings, user.NotificationSettings)
                .Set(u => u.Dob, user.Dob);

            var response = await _db.Users.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
            if(response.ModifiedCount > 0) 
            {
                return new ResponseModel<bool>()
                {
                    IsSuccess = true,
                    Message = "User Profile Updated!"
                };
            }
            else 
            {
                return new ResponseModel<bool>()
                {
                    IsSuccess = false,
                    Message = "Failed To Update User Profile"
                };
            }
        }


    }
}
