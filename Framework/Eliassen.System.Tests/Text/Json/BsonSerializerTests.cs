using Eliassen.System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace Eliassen.System.Tests.Text.Json
{
    public class TargetModel
    {
        public string TargetId { get; set; } = Guid.NewGuid().ToString();
        public DateTimeOffset DTO { get; set; } = DateTimeOffset.Now;
        public DateTime DT { get; set; } = DateTime.Now;

        public DateTimeOffset? DTON { get; set; } = DateTimeOffset.Now;
        public DateTime? DTN { get; set; } = DateTime.Now;
    }

    [TestClass]
    public class BsonSerializerTests
    {
        public TestContext TestContext { get; set; } = null!;

        [TestMethod]
        public void Test()
        {
            var model = new TargetModel
            {
            };

            var options = new JsonSerializerOptions
            {
                TypeInfoResolver = new BsonTypeInfoResolver(),
                WriteIndented = true,
            };
            var json = JsonSerializer.Serialize(model, model.GetType(), options);

            this.TestContext.WriteLine(json);
        }
    }   
}
