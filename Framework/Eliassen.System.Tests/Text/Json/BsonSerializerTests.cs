﻿using Eliassen.System.Text.Json;
using Eliassen.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace Eliassen.System.Tests.Text.Json;

[TestClass]
public class BsonSerializerTests
{
    public TestContext TestContext { get; set; } = null!;

    private static JsonSerializerOptions GetOptions() => new()
    {
        TypeInfoResolver = new BsonTypeInfoResolver(),
        WriteIndented = true,
    };

    [TestMethod]
    [TestCategory(TestCategories.DevLocal)]
    public void Test()
    {
        var model = new TargetModel();
        var json = JsonSerializer.Serialize(model, model.GetType(), GetOptions());

        this.TestContext.WriteLine(json);
    }
}
