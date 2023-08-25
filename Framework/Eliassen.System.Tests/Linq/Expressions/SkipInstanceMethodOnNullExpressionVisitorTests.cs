﻿using Eliassen.System.Linq.Expressions;
using Eliassen.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eliassen.System.Tests.Linq.Expressions;

[TestClass]
public class SkipInstanceMethodOnNullExpressionVisitorTests
{
    public TestContext TestContext { get; set; } = null!;

    [DataTestMethod]
    [TestCategory(TestCategories.Unit)]
    [DataRow("Hello", "Hello", true)]
    [DataRow("Hello", "hello", false)]
    [DataRow(null, "hello", false)]
    [DataRow("Hello", null, false)]
    [DataRow(null, null, false)]
    public void VisitTest_Equals(string input, string matched, bool expected)
    {
        var visitor = new SkipInstanceMethodOnNullExpressionVisitor();

        Expression<Func<string, bool>> expression = e => e.Equals(matched);

        TestContext.WriteLine($"{nameof(expression)}: {expression}");
        var visited = visitor.Visit(expression);
        TestContext.WriteLine($"{nameof(visited)}: {visited}");

        var result = ((LambdaExpression)visited).Compile().DynamicInvoke(input);

        TestContext.WriteLine($"{nameof(result)}: {result}");
        Assert.AreEqual(expected, result);
    }

}
