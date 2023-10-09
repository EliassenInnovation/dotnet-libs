# Eliassen.System


## Class: System.Configuration.CommandLine
builder pattern for command parameter arguments
### Methods


#### AddParameters``1(System.Collections.Generic.IDictionary{System.String,System.String})
add additional configurable parameters
    #####Parameters
    **items:** 

    ##### Return value
    

#### BuildParameters``1
entry point or defining configurable parameters
    ##### Return value
    

## Class: System.IO.StreamJsonDeserializeExtensions
Set of extension method to centralize deserialize of stream using System.Text.Json
### Methods


#### AsJsonAsync``1(System.IO.Stream,System.Text.Json.JsonSerializerOptions)
Convert JSON Stream to .Net Object
    #####Parameters
    **stream:** 

    **options:** 

    ##### Return value
    

#### AsJson``1(System.IO.Stream,System.Text.Json.JsonSerializerOptions)
Convert JSON Stream to .Net Object
    #####Parameters
    **stream:** 

    **options:** 

    ##### Return value
    

#### AsJsonAsync(System.IO.Stream,System.Type,System.Text.Json.JsonSerializerOptions)
Convert JSON Stream to .Net Object
    #####Parameters
    **stream:** 

    **type:** 

    **options:** 

    ##### Return value
    

#### AsJson(System.IO.Stream,System.Type,System.Text.Json.JsonSerializerOptions)
Convert JSON Stream to .Net Object
    #####Parameters
    **stream:** 

    **type:** 

    **options:** 

    ##### Return value
    

## Class: System.IO.StreamXmlDeserializeExtensions
Set of extension method to centralize deserialize of stream using System.Xml
### Methods


#### AsXmlAsync``1(System.IO.Stream,System.Type[])
Convert XML Stream to .Net Object
    #####Parameters
    **stream:** 

    **extraTypes:** 

    ##### Return value
    

#### AsXml``1(System.IO.Stream,System.Type[])
Convert XML Stream to .Net Object
    #####Parameters
    **stream:** 

    **extraTypes:** 

    ##### Return value
    

#### AsXmlAsync(System.IO.Stream,System.Type,System.Type[])
Convert XML Stream to .Net Object
    #####Parameters
    **stream:** 

    **type:** 

    **extraTypes:** 

    ##### Return value
    

#### AsXml(System.IO.Stream,System.Type,System.Type[])
Convert XML Stream to .Net Object
    #####Parameters
    **stream:** 

    **type:** 

    **extraTypes:** 

    ##### Return value
    

## Class: System.Linq.AsyncEnumerableExtensions
Extensions to add async support to existing IEnumerable{T}
### Methods


#### ToListAsync``1(System.Linq.IQueryable{``0},System.Threading.CancellationToken)
Process IQueryable{T} to a List{T} as async
    #####Parameters
    **source:** 

    **cancellationToken:** 

    ##### Return value
    

#### AsEnumerableAsync``1(System.Collections.Generic.IAsyncEnumerable{``0},System.Threading.CancellationToken)
Convert IAsyncEnumerable{TModel} to Task{IEnumerable{TModel}}
    #####Parameters
    **items:** 

    **token:** 

    ##### Return value
    

#### AsAsyncEnumerable``1(System.Threading.Tasks.Task{System.Collections.Generic.IEnumerable{``0}},System.Threading.CancellationToken)
Convert Task{IEnumerable{TModel}} to IAsyncEnumerable{TModel}
    #####Parameters
    **items:** 

    **cancellationToken:** 

    ##### Return value
    

#### AsAsyncEnumerable``1(System.Collections.Generic.IEnumerable{``0},System.Threading.CancellationToken)
Convert IEnumerable{TModel} to IAsyncEnumerable{TModel}
    #####Parameters
    **items:** 

    **cancellationToken:** 

    ##### Return value
    

## Class: System.Linq.DictionaryExtensions
Reusable extensions for Generic Dictionaries
### Methods


#### TryGetValue``2(System.Collections.Generic.IDictionary{``0,``1},``0,``1@,System.Collections.Generic.IEqualityComparer{``0})
extend try get value to allow for using a different IEqualityComparer{TKey}
    #####Parameters
    **dictionary:** 

    **key:** 

    **value:** 

    **comparer:** 

    ##### Return value
    

#### ChangeComparer``2(System.Collections.Generic.IDictionary{``0,``1},System.Collections.Generic.IEqualityComparer{``0})
Rebuild dictionary to use a different IEqualityComparer{TKey}
    #####Parameters
    **dictionary:** 

    **comparer:** 

    ##### Return value
    

## Class: System.Linq.Expressions.ExpressionExtensions
Extensions for System.Linq.Expressions.Expression

## Class: System.Linq.Expressions.SkipInstanceMethodOnNullExpressionVisitor
This visitor will modify expressions to add `x.Property != null` before instance method calls for query rewrite
### Methods


#### VisitMethodCall(System.Linq.Expressions.MethodCallExpression)
If expression is an instance method then modify the expression to ensure a
    #####Parameters
    **node:** 

    ##### Return value
    

## Class: System.Linq.Expressions.StringComparisonReplacementExpressionVisitor
Expression visitor to replace string functions with the matching functions that end with a StringComparison parameter
### Methods


#### VisitBinary(System.Linq.Expressions.BinaryExpression)
Replace `string == string` with `string.Equals(string, StringComparison)`
    #####Parameters
    **node:** 

    ##### Return value
    

#### VisitMethodCall(System.Linq.Expressions.MethodCallExpression)
Replace `string.Xyz(string)` with `string.Xyz(string, StringComparison)`
    #####Parameters
    **input:** 

    ##### Return value
    

## Class: System.Linq.Search.Operators
Expression builder operator options
### Fields

#### Unknown
Unknown
#### EqualTo
Values are equal
#### InSet
Value is within set
#### LessThan
value is less than provided
#### LessThanOrEqualTo
value is less than or equal to provided
#### GreaterThan
value is greater than provided
#### GreaterThanOrEqualTo
value is less greater or equal to provided
#### NotEqualTo
value not equal to

## Class: System.Reflection.ReflectionExtensions
Extensions for reflection and common patterns.
### Fields

#### PublicProperties
flag combination to select public instance properties
#### PublicStaticMethod
flag combination to select public static methods
#### PublicInstanceMethod
flag combination to select public instance methods
### Methods


#### GetKeyValue(System.Object)
lookup key values for provided entity
    #####Parameters
    **item:** 

    ##### Return value
    

#### MakeSafeArray(System.Type,System.Array)
safely create new array for a given element type.
    #####Parameters
    **type:** 

    **inputs:** 

    ##### Return value
    

#### MakeSafe(System.Type,System.Object)
Make safe will try to convert input to target type as best as possible.
    #####Parameters
    **type:** 

    **input:** 

    ##### Return value
    

#### TryParse(System.Type,System.String,System.Object@)
Use best possible match for parsing to provided type
    #####Parameters
    **type:** 

    **toParse:** 

    **parsed:** 

    ##### Return value
    

#### GetShortTypeName(System.Type)
Type name with full namespace, name and assembly. version is not included
    #####Parameters
    **type:** 

    ##### Return value
    

#### GetAttributes(System.Type)
Get all attributes for type
    #####Parameters
    **type:** 

    ##### Return value
    

#### GetAttributes``1(System.Type)
Get all attributes of selected style for reflected type
    #####Parameters
    **type:** 

    ##### Return value
    

#### GetPropertiesByAttribute``1(System.Type)
get property info where attribute matches
    #####Parameters
    **type:** 

    ##### Return value
    

#### GetStaticMethod(System.Type,System.String,System.Type[])
get static method
    #####Parameters
    **type:** 

    **methodName:** 

    **parameterTypes:** 

    ##### Return value
    

#### GetInstanceMethod(System.Type,System.String,System.Type[])
get instance method
    #####Parameters
    **type:** 

    **methodName:** 

    **parameterTypes:** 

    ##### Return value
    

#### GetParametersTypes(System.Reflection.MethodInfo)
get parameters for method
    #####Parameters
    **method:** 

    ##### Return value
    

## Class: System.Reflection.ResourceExtensions
Set of extension methods for embedded resources
### Methods


#### GetResourceStream(System.Object,System.String)
Lookup resource stream based on filename relative the scope of context
    #####Parameters
    **context:** 

    **resourceName:** 

    ##### Return value
    

#### GetResourceStream(System.Type,System.String)
Lookup resource stream based on filename relative the scope of Type
    #####Parameters
    **type:** 

    **resourceName:** 

    ##### Return value
    

#### GetResourceStream(System.Reflection.Assembly,System.String)
Lookup resource stream based on filename relative the scope of Type
    #####Parameters
    **assembly:** 

    **resourceName:** 

    ##### Return value
    

#### GetResourceAsStringAsync(System.Object,System.String)
Lookup resource content based on filename relative the scope of context
    #####Parameters
    **context:** 

    **resourceName:** 

    ##### Return value
    

#### GetResourceAsString(System.Object,System.String)
Lookup resource content based on filename relative the scope of context
    #####Parameters
    **context:** 

    **resourceName:** 

    ##### Return value
    

## Class: System.Security.Claims.ClaimsPrincipalExtensions
Extensions to manage on
### Methods


#### GetAllClaims(System.Security.Claims.ClaimsPrincipal)
Iterate all for provided
    #####Parameters
    **principal:** 

    ##### Return value
    

#### GetClaimValues(System.Security.Claims.ClaimsPrincipal,System.String[])
Get for matched on
    #####Parameters
    **principal:** 

    **claims:** 

    ##### Return value
    

#### GetClaimValue(System.Security.Claims.ClaimsPrincipal,System.String[])
Get first matched on
    #####Parameters
    **principal:** 

    **claims:** 

    ##### Return value
    

## Class: System.Security.Cryptography.Hash
Default hash of input value. Base64 encoded MD5 Hash

## Class: System.ServiceCollectionExtensions
Suggested IOC configurations
### Methods


#### TryAllSystemExtensions(Microsoft.Extensions.DependencyInjection.IServiceCollection,Microsoft.Extensions.Configuration.IConfiguration)
This will add all available extensions to the IOC container
    #####Parameters
    **services:** 

    **config:** 

    ##### Return value
    

#### TryAddSearchQueryExtensions(Microsoft.Extensions.DependencyInjection.IServiceCollection)
Add support for shared SearchQuery Extensions
    #####Parameters
    **services:** 

    ##### Return value
    

#### TrySecurityExtensions(Microsoft.Extensions.DependencyInjection.IServiceCollection)
Add support for shared security extensions
    #####Parameters
    **services:** 

    ##### Return value
    

#### TrySerializerExtensions(Microsoft.Extensions.DependencyInjection.IServiceCollection)
Add support for shared Serializer
    #####Parameters
    **services:** 

    ##### Return value
    

#### TryTemplatingExtensions(Microsoft.Extensions.DependencyInjection.IServiceCollection,Microsoft.Extensions.Configuration.IConfiguration)
Add support for shared Templating
    #####Parameters
    **services:** 

    **config:** 

    ##### Return value
    

#### AddAccessor``1(Microsoft.Extensions.DependencyInjection.IServiceCollection)
Register accessor type that is scoped to as AsyncLocal
    #####Parameters
    **services:** 

    ##### Return value
    

#### AddConfiguration``1(Microsoft.Extensions.DependencyInjection.IServiceCollection,Microsoft.Extensions.Configuration.IConfiguration)
Extend configuration options
    #####Parameters
    **services:** 

    **config:** 

    ##### Return value
    

#### GetSingletonInstance``2(Microsoft.Extensions.DependencyInjection.IServiceCollection,``1@)
Get singleton instance from IOC container
    #####Parameters
    **services:** 

    **instance:** 

    ##### Return value
    

## Class: System.Text.Json.BsonDateConverter
System.Text.Json converter to support BsonDatetimeOffset

## Class: System.Text.Json.BsonIdConverter
This type converter for System.Text.Json to support BSON ObjectID to JSON safe export/import

## Class: System.Text.Json.JsonDocumentExtensions
shared extension methods for System.Text.Json
### Methods


#### ToXmlDocument(System.Text.Json.JsonElement,System.String)
Convert System.Test.Json.JsonDocument to System.Xml.XmlDocument
    #####Parameters
    **json:** 

    **rootName:** 

    ##### Return value
    

## Class: System.Text.Json.Serialization.DefaultJsonSerializer
Default serializer for JSON
### Methods


#### AsPropertyName(System.String)
Use the configured property naming policy to change provided value
    #####Parameters
    **propertyName:** 

    ##### Return value
    

## Class: System.Text.Templating.FileTemplateSource
Access template from file system

## Class: System.Text.Templating.FileTemplatingSettings
Configuration settings for file templating engine
### Properties

#### TemplatePath
template source path
#### SandboxPath
sandbox root path
#### Priority
template priority

## Class: System.Text.Templating.TemplateEngine
Generate templating engine that will try to use best match for source and provider