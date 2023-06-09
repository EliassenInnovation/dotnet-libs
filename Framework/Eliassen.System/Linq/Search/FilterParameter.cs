﻿using System.Text;
using System.Text.Json.Serialization;

namespace Eliassen.System.Linq.Search
{
    /// <summary>
    /// Filter parameter
    /// </summary>
    public record FilterParameter
    {
        /// <summary>
        /// `Equal To`: pass in the value to match for a given property  
        /// 
        /// If you are using string values you may also use wild cards  
        /// \*bc -> Ends with  
        /// \*b\* -> Contains  
        /// ab\* -> Starts with
        /// </summary>
        [JsonPropertyName("eq")]
        public object? EqualTo { get; set; }

        /// <summary>
        /// `Not Equal To`: pass in the value to match for a given property  
        /// 
        /// If you are using string values you may also use wild cards  
        /// \*bc -> Ends with  
        /// \*b\* -> Contains  
        /// ab\* -> Starts with
        /// </summary>
        [JsonPropertyName("neq")]
        public object? NotEqualTo { get; set; }

        /// <summary>
        /// This allows for providing a set of values where the value from the queries data must match at least 
        /// one of provided values
        /// </summary>
        [JsonPropertyName("in")]
        public object?[]? InSet { get; set; }

        /// <summary>
        /// `Greater than`
        /// </summary>
        [JsonPropertyName("gt")]
        public object? GreaterThan { get; set; }
        /// <summary>
        /// `Greater than or equal to`
        /// </summary>
        [JsonPropertyName("gte")]
        public object? GreaterThanOrEqualTo { get; set; }
        /// <summary>
        /// `Less than`
        /// </summary>
        [JsonPropertyName("lt")]
        public object? LessThan { get; set; }
        /// <summary>
        /// `Less than or equal to`
        /// </summary>
        [JsonPropertyName("lte")]
        public object? LessThanOrEqualTo { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder();
            if (EqualTo != null) sb.AppendLine($"{nameof(EqualTo)}: {EqualTo} ");
            if (NotEqualTo != null) sb.AppendLine($"{nameof(NotEqualTo)}: {NotEqualTo} ");
            if (InSet != null) sb.AppendLine($"{nameof(InSet)}: {string.Join("; ", InSet)} ");
            if (GreaterThan != null) sb.AppendLine($"{nameof(GreaterThan)}: {GreaterThan} ");
            if (GreaterThanOrEqualTo != null) sb.AppendLine($"{nameof(GreaterThanOrEqualTo)}: {GreaterThanOrEqualTo} ");
            if (LessThan != null) sb.AppendLine($"{nameof(LessThan)}: {LessThan} ");
            if (LessThanOrEqualTo != null) sb.AppendLine($"{nameof(LessThanOrEqualTo)}: {LessThanOrEqualTo} ");
            return sb.ToString().Trim();
        }
    }
}
