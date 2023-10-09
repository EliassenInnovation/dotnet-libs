using Eliassen.MongoDB.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Nucleus.Core.Shared.Persistence.Services.ServiceHelpers;
using System;

namespace Nucleus.Core.Shared.Tests.Persistence.Services.ServiceHelpers
{
    public class TargetModel
    {
        public string Name { get; set; }
        public string TargetId { get; set; } = Guid.NewGuid().ToString();
    }
    public class TargetCollection
    {
        // [BsonElement("Name")]
        public string? Name { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TargetId { get; set; } = Guid.NewGuid().ToString();
    }


    [TestClass]
    public class BsonCollectionBuilderTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void BuildCollectionTest()
        {
            var f = new MongoDatabaseFactory(null);

            var model = new TargetModel { Name = "hello", };

            var builder = new BsonCollectionBuilder<TargetModel, TargetCollection>();
            var collection = builder.BuildCollection(model);

            TestContext.WriteLine($"model: {model.Name}");
            TestContext.WriteLine($"collection: {collection.Name}");
            Assert.AreEqual(model.Name, collection.Name);
        }

        [TestMethod]
        public void BuildCollectionStringTest()
        {
            var f = new MongoDatabaseFactory(null);
            /*
 MongoDatabaseFactory : IMongoDatabaseFactory
            */

            var model = new TargetModel { Name = "hello", };
            var builder = new BsonCollectionBuilder<TargetModel, TargetCollection>();
            var text = builder.BuildCollectionString(model);

            TestContext.WriteLine(text);
        }

        /*
    public class BsonCollectionBuilder<T,TCollection>
    {
        public TCollection BuildCollection(T item)
        {
            var jsonDoc = JsonConvert.SerializeObject(item, new JsonSerializerSettings() { ContractResolver = BsonContractResolver.Instance });
            return BsonSerializer.Deserialize<TCollection>(jsonDoc);
        }

        public string BuildCollectionString(T item) =>
            JsonConvert.SerializeObject(item, new JsonSerializerSettings() { ContractResolver = BsonContractResolver.Instance });
    }
        */
    }
}
