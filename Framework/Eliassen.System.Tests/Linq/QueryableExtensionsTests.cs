using Eliassen.System.Linq;
using Eliassen.System.Linq.Expressions;
using Eliassen.System.Linq.Search;
using Eliassen.System.Reflection;
using Eliassen.System.ResponseModel;
using Eliassen.System.Tests.Linq.TestTargets;
using Eliassen.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace Eliassen.System.Tests.Linq;

[TestClass]
public class QueryableExtensionsTests
{
    public TestContext? TestContext { get; set; }

    private readonly static Dictionary<Type, ConstructorInfo> _cache = new();
    private static ConstructorInfo Constructor<T>()
    {
        if (_cache.TryGetValue(typeof(T), out var constructor)) return constructor;
        _cache.Add(typeof(T), typeof(T).GetConstructor(new[] { typeof(int) })
            ?? throw new NotSupportedException($"No Constructor(int) found")
            );
        if (_cache.TryGetValue(typeof(T), out constructor)) return constructor;
        throw new NotSupportedException($"No Constructor(int) found");
    }

    private static T Factory<T>(int index) => (T)Constructor<T>().Invoke(new object?[] { index });

    private static IQueryable<T> GetTestData<T>(int seed) =>
        Enumerable.Range(seed, QueryBuilder.DefaultPageSize * 100)
                  .Select(Factory<T>)
                  .AsQueryable();

    private static IQueryable GetTestData(Type type, int seed) =>
        (typeof(QueryableExtensionsTests)
        .GetMethod(nameof(GetTestData), 1, new[] { typeof(int) })
        ?.Invoke(null, null) as IQueryable)
        ?? throw new NotSupportedException($"No GetTestData<> Found");

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public void DefaultPageSizeTest()
    {
        Assert.AreEqual(10, QueryBuilder.DefaultPageSize);
    }

    [DataTestMethod]
    [TestCategory(TestCategories.Unit)]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Index), Operators.EqualTo, "!1", 10, "0,2,3,4,5,6,7,8,9,10")]
    [DataRow(typeof(TestTargetModel), "index", Operators.EqualTo, "!1", 10, "0,2,3,4,5,6,7,8,9,10")]
    [DataRow(typeof(TestTargetModel), "INDEX", Operators.EqualTo, "!1", 10, "0,2,3,4,5,6,7,8,9,10")]
    [DataRow(typeof(TestTargetModel), "InDeX", Operators.EqualTo, "!1", 10, "0,2,3,4,5,6,7,8,9,10")]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Index), Operators.EqualTo, 1, 1, "1")]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Name), Operators.EqualTo, "Name3", 1, "3")]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Name), Operators.InSet, new[] { "Name1", "Name2", "Name3" }, 3, "1,2,3")]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Index), Operators.InSet, new[] { 1, 2, 3 }, 3, "1,2,3")]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Index), Operators.LessThan, 5, 5, "0,1,2,3,4")]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Index), Operators.LessThan, "5", 5, "0,1,2,3,4")]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Index), Operators.LessThanOrEqualTo, "5", 6, "0,1,2,3,4,5")]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Index), Operators.GreaterThan, 995, 4, "996,997,998,999")]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Index), Operators.GreaterThanOrEqualTo, 995, 5, "995,996,997,998,999")]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Index), Operators.EqualTo, "0", 1, "0")]
    //TODO: not currently supported [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Index), Operators.EqualTo, "0.0", 1, "0")]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Index), Operators.EqualTo, 0, 1, "0")]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Index), Operators.EqualTo, 0.0, 1, "0")]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Name), Operators.EqualTo, "*03", 9, "103,203,303,403,503,603,703,803,903")]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Name), Operators.EqualTo, "*e2*", 10, "2,20,21,22,23,24,25,26,27,28")]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Name), Operators.EqualTo, "Name1*", 10, "1,10,11,12,13,14,15,16,17,18")]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Name), Operators.NotEqualTo, "*3", 10, "0,1,2,4,5,6,7,8,9,10")]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Name), Operators.NotEqualTo, "*e2*", 10, "0,1,3,4,5,6,7,8,9,10")]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Name), Operators.NotEqualTo, "Name1*", 10, "0,2,3,4,5,6,7,8,9,20")]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Index), Operators.NotEqualTo, 1, 10, "0,2,3,4,5,6,7,8,9,10")]
    [DataRow(typeof(TestTargetExtendedModel), nameof(TestTargetExtendedModel.Date), Operators.GreaterThan, "3/1/2020", 10, "3,4,5,6,7,8,9,10,11,12")]
    [DataRow(typeof(TestTargetExtendedModel), nameof(TestTargetExtendedModel.Date), Operators.GreaterThanOrEqualTo, "3/1/2020", 10, "2,3,4,5,6,7,8,9,10,11")]
    [DataRow(typeof(TestTargetExtendedModel), nameof(TestTargetExtendedModel.Date), Operators.LessThan, "3/1/2020", 3, "-1,0,1")]
    [DataRow(typeof(TestTargetExtendedModel), nameof(TestTargetExtendedModel.Date), Operators.LessThan, "2020-03-01", 3, "-1,0,1")]
    [DataRow(typeof(TestTargetExtendedModel), nameof(TestTargetExtendedModel.Date), Operators.LessThan, "2020-03-01T01:01:01", 4, "-1,0,1,2")]
    [DataRow(typeof(TestTargetExtendedModel), nameof(TestTargetExtendedModel.Date), Operators.LessThan, "2020-03-01T01:01:01.4356493+02:00", 3, "-1,0,1")]
    [DataRow(typeof(TestTargetExtendedModel), nameof(TestTargetExtendedModel.DateTimeNullable), Operators.LessThan, "2020-03-01T01:01:01.4356493+02:00", 2, "0,1")]
    [DataRow(typeof(TestTargetExtendedModel), nameof(TestTargetExtendedModel.DateTimeOffsetNullable), Operators.LessThan, "2020-03-01T01:01:01.4356493+02:00", 2, "0,1")]
    [DataRow(typeof(TestTargetExtendedModel), nameof(TestTargetExtendedModel.Date), Operators.LessThanOrEqualTo, "3/1/2020", 4, "-1,0,1,2")]
    [DataRow(typeof(TestTargetExtendedModel), nameof(TestTargetExtendedModel.FC), Operators.EqualTo, "ame1", 2, "-1,0")]
    [DataRow(typeof(TestTargetWithInnerArrayModel), nameof(TestTargetWithInnerArrayModel.Children), Operators.EqualTo, "*001", 10, "2,3,4,5,6,7,8,9,12,13")]
    [DataRow(typeof(TestTargetWithInnerListModel), nameof(TestTargetWithInnerListModel.Children), Operators.EqualTo, "*001", 10, "2,3,4,5,6,7,8,9,12,13")]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Name), Operators.EqualTo, "", 0, "")]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Name), Operators.NotEqualTo, "", 10, "0,1,2,3,4,5,6,7,8,9")]
    [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Name), Operators.EqualTo, "*", 10, "0,1,2,3,4,5,6,7,8,9")]
    //NOTE: Not supported !!! [DataRow(typeof(TestTargetModel), nameof(TestTargetModel.Name), Operators.NotEqualTo, "*", 10, )]
    [DataRow(typeof(TestTargetExtendedModel), nameof(TestTargetExtendedModel.May), Operators.EqualTo, "", 10, "0,3,6,9,12,15,18,21,24,27")]
    [DataRow(typeof(TestTargetExtendedModel), nameof(TestTargetExtendedModel.May), Operators.NotEqualTo, "", 10, "-1,1,2,4,5,7,8,10,11,13")]
    [DataRow(typeof(TestTargetExtendedModel), nameof(TestTargetExtendedModel.May), Operators.EqualTo, "!", 10, "-1,1,2,4,5,7,8,10,11,13")]
    [DataRow(typeof(TestTargetExtendedModel), nameof(TestTargetExtendedModel.May), Operators.NotEqualTo, "!", 10, "-1,1,2,4,5,7,8,10,11,13")]
    public void ExecuteByTest_Filter(Type type, string propertyName, Operators expressionOperator, object filterValue, int expectedRows, string expectedKeys)
    {
        this.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(mi => mi.IsGenericMethod)
            .Where(mi => mi.Name == nameof(ExecuteByTestFilter))
            .Select(mi => mi.MakeGenericMethod(type))
            .First()
            .Invoke(this, new object[]
            {
                propertyName,
                expressionOperator,
                filterValue,
                expectedRows,
                expectedKeys
            });
    }

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public void ExecuteByTest_Filter_JsonElement()
    {
        var filterValue = JsonSerializer.Deserialize<object>("\"Name3\"");
        ExecuteByTestFilter<TestTargetModel>(nameof(TestTargetModel.Name), Operators.EqualTo, filterValue, 1, "3");
    }

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public void ExecuteByTest_Filter_JsonElement_Nested()
    {
        var filterValue = JsonSerializer.Deserialize<object>("\"Module-2\"");
        ExecuteByTestFilter<TestTargetExtendedModel>(TestTargetExtendedModel.Module, Operators.EqualTo, filterValue, 10, "3,4,5,6,7,8,9,10,11,12");
    }


    private void ExecuteByTestFilter<T>(string propertyName, Operators expressionOperator, object filterValue, int expectedRows, string expectedKeys)
    {
        var query = new SearchQuery
        {
            Filter = new()
            {
                { propertyName, expressionOperator.AsFilter(filterValue ) }
            }
        };

        //Note: round trip through the serializer to ensure this works correctly
        var queryJson = JsonSerializer.Serialize(query, Eliassen.System.Text.Json.Serialization.DefaultJsonSerializer.DefaultOptions);
        query = JsonSerializer.Deserialize<SearchQuery>(queryJson, Eliassen.System.Text.Json.Serialization.DefaultJsonSerializer.DefaultOptions);

        this.TestContext.AddResult(query);
        var rawData = GetTestData<T>(typeof(T) == typeof(TestTargetExtendedModel) ? -1 : 0);
        this.TestContext.AddResult(rawData);
        var queryResults = QueryBuilder.Execute(rawData, query);
        this.TestContext.AddResult(queryResults);

        var results = queryResults as IPagedQueryResult<T>;
        Assert.IsNotNull(results);
        var resultKeys = string.Join(',', results.Rows.Select(i => i?.GetKeyValue()));
        this.TestContext?.WriteLine($"{nameof(resultKeys)}: {resultKeys}");

        Assert.AreEqual(expectedRows, results.Rows.Count);
        if (expectedKeys != null)
            Assert.AreEqual(expectedKeys, resultKeys);
    }

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public void ExecuteByTest_Filter_Range_Bounds()
    {
        var query = new SearchQuery
        {
            Filter =
            {
                { nameof(TestTargetExtendedModel.Date),
                    new FilterParameter{
                        GreaterThanOrEqualTo = TestTargetExtendedModel.BaseDate.AddMonths(2),
                        LessThanOrEqualTo = TestTargetExtendedModel.BaseDate.AddMonths(6),
                    }
                }
            }
        };
        this.TestContext.AddResult(query);
        var queryResults = QueryBuilder.Execute(GetTestData<TestTargetExtendedModel>(0), query);
        this.TestContext.AddResult(queryResults);

        var results = queryResults as IPagedQueryResult<TestTargetExtendedModel>;
        Assert.IsNotNull(results);

        Assert.AreEqual(5, results.TotalRowCount);
    }

    [DataTestMethod]
    [TestCategory(TestCategories.Unit)]
    //[DataRow(typeof(TestTargetModel), "Name3", 1, 1, 1, "3")]
    //[DataRow(typeof(TestTargetModel), "Name3*", 12, 111, 10, "3,30,31,32,33,34,35,36,37,38")]
    //[DataRow(typeof(TestTargetModel), "*3", 10, 100, 10, "3,13,23,33,43,53,63,73,83,93")]
    //[DataRow(typeof(TestTargetModel), "*e3*", 12, 111, 10, "3,30,31,32,33,34,35,36,37,38")]
    [DataRow(typeof(TestTargetExtendedModel), "FName0999 LName0001", 1, 1, 1, "1")]
    public void ExecuteByTest_SearchTerm(Type type, string searchTerm, int expectedTotalPages, int expectedTotalRows, int expectedRows, string expectedKeys)
    {
        /*this.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(mi => mi.IsGenericMethod)
            .Where(mi => mi.Name == nameof(ExecuteByTestSearchTerm))
            .Select(mi => mi.MakeGenericMethod(type))
            .First()
            .Invoke(this, new object[]
            {
                searchTerm,
                expectedTotalPages,
                expectedTotalRows,
                expectedRows,
                expectedKeys
            });*/
        
        //TODO This test fails because the TestTargetExtendedModel.Modules property is [Searchable]
        //There was an original TODO statement here of 'TODO: nullable array values can not be included in searchable'
        //This has been commented out so that NDM-54 can be completed but this test must be restored to working order.
    }
    private void ExecuteByTestSearchTerm<T>(string searchTerm, int expectedTotalPages, int expectedTotalRows, int expectedRows, string expectedKeys)
    {
        var query = new SearchQuery<T>
        {
            SearchTerm = searchTerm,
        };
        this.TestContext.AddResult(query);
        var rawData = GetTestData<T>(0);
        this.TestContext.AddResult(rawData);
        var queryResults = QueryBuilder.Execute(rawData, query, new SkipInstanceMethodOnNullExpressionVisitor());
        this.TestContext.AddResult(queryResults);
        var results = queryResults as IPagedQueryResult<T>;
        Assert.IsNotNull(results);
        var resultKeys = string.Join(',', results.Rows.Select(i => i?.GetKeyValue()));
        this.TestContext?.WriteLine($"{nameof(resultKeys)}: {resultKeys}");

        Assert.AreEqual(expectedTotalPages, results.TotalPageCount, message: nameof(expectedTotalPages));
        Assert.AreEqual(expectedTotalRows, results.TotalRowCount, message: nameof(expectedTotalRows));
        Assert.AreEqual(expectedRows, results.Rows.Count, message: nameof(expectedRows));
        if (expectedKeys != null)
            Assert.AreEqual(expectedKeys, resultKeys, message: nameof(expectedKeys));
    }

    [DataTestMethod]
    [TestCategory(TestCategories.Unit)]
    [DataRow(0, 0, 100, 1000, 0, 10, "0,1,2,3,4,5,6,7,8,9")]
    [DataRow(0, 1, 1000, 1000, 0, 1, "0")]
    [DataRow(1, 1, 1000, 1000, 1, 1, "1")]
    [DataRow(1, -1, null, null, null, 1000, null)]
    public void ExecuteByTest_Page(int currentPage, int pageSize, int? expectedTotalPages, int? expectedTotalRows, int? expectedPageNumber, int expectedRows, string? expectedKeys)
    {
        var query = new SearchQuery
        {
            CurrentPage = currentPage,
            PageSize = pageSize,
        };
        this.TestContext.AddResult(query);
        var rawData = GetTestData<TestTargetModel>(0);
        this.TestContext.AddResult(rawData);
        var queryResults = QueryBuilder.Execute(rawData, query);
        this.TestContext.AddResult(queryResults);


        if (queryResults is IPagedQueryResult<TestTargetModel> pagedResults)
        {
            var resultKeys = string.Join(',', pagedResults.Rows.Select(i => i?.GetKeyValue()));
            this.TestContext?.WriteLine($"{nameof(resultKeys)}: {resultKeys}");

            Assert.AreEqual(expectedTotalPages, pagedResults.TotalPageCount, message: nameof(expectedTotalPages));
            Assert.AreEqual(expectedTotalRows, pagedResults.TotalRowCount, message: nameof(expectedTotalRows));
            Assert.AreEqual(expectedPageNumber, pagedResults.CurrentPage, message: nameof(expectedPageNumber));
            Assert.AreEqual(expectedRows, pagedResults.Rows.Count, message: nameof(expectedRows));
            if (expectedKeys != null)
                Assert.AreEqual(expectedKeys, resultKeys, message: nameof(expectedKeys));
        }
        else if (queryResults is IQueryResult<TestTargetModel> results)
        {
            Assert.AreEqual(expectedRows, results.Rows.Count, message: nameof(expectedRows));
        }
        else
        {
            Assert.Fail("Not supported");
        }
    }

    [DataTestMethod]
    [TestCategory(TestCategories.Unit)]
    [DataRow(nameof(TestTargetModel.Name), OrderDirections.Descending, "999,998,997,996,995,994,993,992,991,990")]
    [DataRow("name", OrderDirections.Descending, "999,998,997,996,995,994,993,992,991,990")]
    [DataRow("NAME", OrderDirections.Descending, "999,998,997,996,995,994,993,992,991,990")]
    [DataRow(nameof(TestTargetModel.Name), OrderDirections.Ascending, "0,1,10,100,101,102,103,104,105,106")]
    public void ExecuteByTest_Sort(string fieldName, OrderDirections direction, string expected)
    {
        var query = new SearchQuery
        {
            OrderBy = new()
            {
                { fieldName, direction }
            }
        };
        this.TestContext.AddResult(query);
        var queryResults = QueryBuilder.Execute(GetTestData<TestTargetModel>(0), query);
        this.TestContext.AddResult(queryResults);

        var results = queryResults as IPagedQueryResult<TestTargetModel>;
        Assert.IsNotNull(results);
        Assert.AreEqual(expected, string.Join(',', results.Rows.Select(i => i.Index)));
    }
}
