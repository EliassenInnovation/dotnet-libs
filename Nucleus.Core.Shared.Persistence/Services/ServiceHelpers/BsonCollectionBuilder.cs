
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;

namespace Nucleus.Core.Shared.Persistence.Services.ServiceHelpers
{
    public class BsonCollectionBuilder<T, TCollection>
    {
        public TCollection BuildCollection(T item)
        {
            var jsonDoc = BuildCollectionString(item);
            return BsonSerializer.Deserialize<TCollection>(jsonDoc);
        }

        public string BuildCollectionString(T item) =>
            JsonConvert.SerializeObject(item, new JsonSerializerSettings()
            {
                ContractResolver = BsonContractResolver.Instance
            });

    }
}
