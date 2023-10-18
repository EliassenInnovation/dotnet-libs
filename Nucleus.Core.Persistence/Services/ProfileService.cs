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

        public async Task<bool> UpdateUserProfile(User user)
        {
            var filter = Builders<UserCollection>.Filter.Eq(u => u.UserId, user.UserId);
            var response = await _db.Users.ReplaceOneAsync(filter, _userCollectionBuilder.BuildCollection(user));
            if(response.ModifiedCount > 0) 
            {
                return true;
            }
            else 
            {  
                return false; 
            }
        }


    }
}
