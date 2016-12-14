// ***********************************************************************
// <copyright file="FilterModel.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Collections.Generic;

namespace KoreAsp.Service.Request
{
    /// <summary>
    /// Encapsulates the service request filter information
    /// </summary>
    public class FilterModel
    {
        /// <summary>
        /// Gets or sets the field.
        /// </summary>
        /// <value>The field.</value>
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>The operator.</value>
        public FilterOperator Operator { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the type of the value.
        /// </summary>
        /// <value>The type of the value.</value>
        public string ValueType { get; set; }

        /// <summary>
        /// Gets or sets the logic.
        /// </summary>
        /// <value>The logic.</value>
        public FilterLogic Logic { get; set; }

        /// <summary>
        /// Gets or sets the child filters.
        /// </summary>
        /// <value>The child filters.</value>
        public IEnumerable<FilterModel> ChildFilters { get; set; }
    }

    /// <summary>
    /// A set of filter operators that could be used in the where clause of a query
    /// </summary>
    public enum FilterOperator
    {
        /// <summary>
        /// Operator for <code>Field == Value</code>
        /// </summary>
        Equals = 0,

        /// <summary>
        /// Operator for <code>Field != Value</code>
        /// </summary>
        DoesNotEqual = 1,

        /// <summary>
        /// Operator for <code>Field.Contains(Value)</code>
        /// </summary>
        Contains = 2,

        /// <summary>
        /// Operator for <code>!Field.Contains(Value)</code>
        /// </summary>
        DoesNotContain = 3,

        /// <summary>
        /// Operator for <code>Field.StartsWith(Value)</code>
        /// </summary>
        StartsWith = 4,

        /// <summary>
        /// Operator for <code>Field.EndsWith(Value)</code>
        /// </summary>
        EndsWith = 5,

        /// <summary>
        /// Operator for <code>Field == null</code>
        /// </summary>
        IsBlank = 6,

        /// <summary>
        /// Operator for <code>Field != null</code>
        /// </summary>
        IsNotBlank = 7,

        /// <summary>
        /// Operator for <code>Field &lt; Value</code>
        /// </summary>
        LessThan = 8,

        /// <summary>
        /// Operator for <code>Field &gt; Value</code>
        /// </summary>
        GreaterThan = 9,

        /// <summary>
        /// Operator for <code>Field &lt;= Value</code>
        /// </summary>
        LessThanOrEqualTo = 10,

        /// <summary>
        /// Operator for <code>Field &gt;= Value</code>
        /// </summary>
        GreaterThanOrEqualTo = 11
    }

    /// <summary>
    /// Defines how the child filters of a filter should be logically combined
    /// </summary>
    public enum FilterLogic
    {
        /// <summary>
        /// Combine filters using AND logic
        /// </summary>
        And,

        /// <summary>
        /// Combine filters using OR logic
        /// </summary>
        Or
    }
}
