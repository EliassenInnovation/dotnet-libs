using MongoDB.Driver;
using Nucleus.Core.Contracts.Models;
using Nucleus.Core.Contracts.Persistence;
using Nucleus.Core.Persistence.Collections;
using Nucleus.Core.Shared.Persistence.Services.ServiceHelpers;
using System.Linq;
using System.Threading.Tasks;

namespace Nucleus.Core.Persistence.Services
{
    public class ProfileService : IProfileService
    {
        private readonly ICoreMongoDatabase _db;
        private readonly ProjectionDefinition<UserCollection, User>? _userProjection;
        private readonly BsonCollectionBuilder<User, UserCollection> _userCollectionBuilder;

        public ProfileService(ICoreMongoDatabase db)
        {
            _db = db;
            _userCollectionBuilder = new BsonCollectionBuilder<User, UserCollection>();
            _userProjection = Builders<UserCollection>.Projection.Expression(Projections.Users);
        }

        public IQueryable<User> Query() => _db.Users.AsQueryable().Select(Projections.Users);


        public async Task<ResponseModel<bool>> UpdateUserProfile(User user)
        {
            var filter = Builders<UserCollection>.Filter.Eq(u => u.UserId, user.UserId);
            var update = Builders<UserCollection>.Update
                .Set(u => u.FirstName, user.FirstName)
                .Set(u => u.LastName, user.LastName)
                .Set(u => u.Active, user.Active)
                .Set(u => u.AddressLine1, user.AddressLine1)
                .Set(u => u.AddressLine2, user.AddressLine2)
                .Set(u => u.PostalCode, user.PostalCode)
                .Set(u => u.Bio, user.Bio)
                .Set(u => u.City, user.City)
                .Set(u => u.Country, user.Country)
                .Set(u => u.StateProvince, user.StateProvince)
                .Set(u => u.PhoneNumber, user.PhoneNumber)
                .Set(u => u.NotificationSettings, user.NotificationSettings)
                .Set(u => u.Dob, user.Dob);

            var response = await _db.Users.UpdateOneAsync(filter, update);
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

        public async Task<ResponseModel<User>> GetUserProfile(string userName)
        {
            var user = await Task.FromResult(Query().FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower()));

            if (user != null)
            {
                return new ResponseModel<User>()
                {
                    IsSuccess = true,
                    Response = user
                };
            }
            else
            {
                return new ResponseModel<User>()
                {
                    IsSuccess = false,
                    Message = "Failed To Fetch User Profile"
                };
            }
        }


    }
}
