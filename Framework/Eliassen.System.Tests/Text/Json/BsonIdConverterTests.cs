using Eliassen.System.Tests.Text.Json.TestTargets;
using Eliassen.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace Eliassen.System.Tests.Text.Json;

[TestClass]
public class BsonIdConverterTests
{
    public TestContext TestContext { get; set; } = null!;


    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public void SerializeTest()
    {
        var expected = "Hello World";

        var result = JsonSerializer.Serialize(new TestIdProperty
        {
            ProjectId = expected,
        });

        this.TestContext.AddResult(result, fileName: "result.json");

        var document = JsonDocument.Parse(result);
        var selected = document.RootElement.GetProperty("_id").GetProperty("$oid").GetString();
        this.TestContext.WriteLine(selected);

        Assert.AreEqual(expected, selected);
    }

    [DataTestMethod]
    [TestCategory(TestCategories.Unit)]
    [DataRow(@"{""_id"":{""$oid"":""Hello World""}}", "Hello World")]
    [DataRow(@"{""_id"":""Hello World""}", "Hello World")]
    public void DeserializeTest(string input, string expected)
    {
        this.TestContext.AddResult(input, fileName: "input.json");
        var result = JsonSerializer.Deserialize<TestIdProperty>(input);
        this.TestContext.AddResult(result, fileName: "result.json");
        Assert.AreEqual(expected, result?.ProjectId);
    }
}
