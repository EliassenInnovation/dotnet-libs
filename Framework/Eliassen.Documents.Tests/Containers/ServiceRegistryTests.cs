﻿using Eliassen.Documents.Tests.TestClasses;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Eliassen.Documents.Tests.Containers;

[TestClass]
public class ServiceRegistryTests
{
    public required TestContext TestContext { get; set; }

    [TestMethod]
    public void Create_IBlobContainer__ContainerTargetClass_Test()
    {
        var services = new ServiceCollection()
            .TryAddDocumentServices()
            .BuildServiceProvider();

        var wrapper = services.GetRequiredService<IBlobContainer<ContainerTargetClass>>();
    }

    [TestMethod]
    public void Create_IBlobContainer__Keyed_Test()
    {
        var services = new ServiceCollection()
            .TryAddDocumentServices()
            .BuildServiceProvider();

        var wrapper = services.GetKeyedService<IBlobContainer>("TestName");
    }
}
