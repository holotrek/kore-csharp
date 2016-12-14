// ***********************************************************************
// <copyright file="ResponseExtensions.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using KoreAsp.Providers.Serialization;
using KoreAsp.Service.Request;

namespace KoreAsp.Extensions
{
    /// <summary>
    /// A set of extensions for the REST service layer
    /// </summary>
    public static class ResponseExtensions
    {
        /// <summary>
        /// Pages and sorts a query.
        /// </summary>
        /// <typeparam name="T">The type of the entity within the query.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="request">The request.</param>
        /// <returns>The paged and sorted query.</returns>
        public static IEnumerable<T> PageAndSort<T>(this IEnumerable<T> query, GetCollectionRequest request)
        {
            return query.ApplyPagingAndSorting(request);
        }

        /// <summary>
        /// Filters a query.
        /// </summary>
        /// <typeparam name="T">The type of the entity within the query.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="request">The request.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns>The filtered query.</returns>
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> query, GetCollectionRequest request, ISerializationProvider serializer)
        {
            return query.ApplyFilters(request, serializer);
        }

        /// <summary>
        /// Applies the paging.
        /// </summary>
        /// <typeparam name="T">The type of the entity within the query.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="request">The request.</param>
        /// <returns>The paged and sorted query.</returns>
        private static IEnumerable<T> ApplyPagingAndSorting<T>(this IEnumerable<T> query, GetCollectionRequest request)
        {
            if (request != null)
            {
                Type t = typeof(T);
                if (t.IsPrimitive)
                {
                    query = query.OrderBy(x => x);
                }
                else if (request.Sort != null && request.Sort.Count() > 0)
                {
                    string orderClause = string.Join(", ", request.Sort);
                    query = query.OrderBy(orderClause);
                }

                if (request.Take == 0)
                {
                    request.Take = request.PageSize;
                }

                if (request.Take == 0)
                {
                    request.Take = 10;
                }

                if (request.Page > 0)
                {
                    request.Skip = (request.Page - 1) * request.PageSize;
                }

                query = query.Skip(request.Skip).Take(request.Take);
            }

            return query;
        }

        /// <summary>
        /// Applies the filters.
        /// </summary>
        /// <typeparam name="T">The type of the entity within the query.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="request">The request.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns>The filtered query.</returns>
        private static IEnumerable<T> ApplyFilters<T>(this IEnumerable<T> query, GetCollectionRequest request, ISerializationProvider serializer)
        {
            if (string.IsNullOrWhiteSpace(request.Filter))
            {
                return query;
            }

            FilterModel filter = null;
            try
            {
                filter = serializer.DeserializeObject<FilterModel>(request.Filter);
            }
            catch
            {
            }

            if (filter == null)
            {
                return query;
            }

            int paramCount = 0;
            List<object> values = new List<object>();
            string clause = query.ApplySingleFilter(filter, ref paramCount, ref values);
            return query.Where(clause, values.ToArray());
        }

        /// <summary>
        /// Applies the single filter.
        /// </summary>
        /// <typeparam name="T">The type of the entity within the query.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="paramCount">The parameter count.</param>
        /// <param name="values">The values.</param>
        /// <returns>The filter clause.</returns>
        private static string ApplySingleFilter<T>(this IEnumerable<T> query, FilterModel filter, ref int paramCount, ref List<object> values)
        {
            string filterClause = string.Empty;
            if (filter.ChildFilters != null && filter.ChildFilters.Count() > 0)
            {
                filterClause = string.Format("({0})", query.ApplyFilters(filter.Logic, filter.ChildFilters, ref paramCount, ref values));
            }
            else
            {
                string param = "@" + paramCount;
                bool iterateParam = true;

                if (filter.Field != null)
                {
                    switch (filter.Operator)
                    {
                        case FilterOperator.Equals:
                            filterClause = "{0} == " + param;
                            break;
                        case FilterOperator.DoesNotEqual:
                            filterClause = "{0} != " + param;
                            break;
                        case FilterOperator.Contains:
                            filterClause = "{0}.Contains(" + param + ")";
                            break;
                        case FilterOperator.DoesNotContain:
                            filterClause = "!{0}.Contains(" + param + ")";
                            break;
                        case FilterOperator.StartsWith:
                            filterClause = "{0}.StartsWith(" + param + ")";
                            break;
                        case FilterOperator.EndsWith:
                            filterClause = "{0}.EndsWith(" + param + ")";
                            break;
                        case FilterOperator.IsBlank:
                            filterClause = "({0} == null || {0} == '')";
                            iterateParam = false;
                            break;
                        case FilterOperator.IsNotBlank:
                            filterClause = "({0} != null && {0} != '')";
                            iterateParam = false;
                            break;
                        case FilterOperator.LessThan:
                            filterClause = "{0} < " + param;
                            break;
                        case FilterOperator.GreaterThan:
                            filterClause = "{0} > " + param;
                            break;
                        case FilterOperator.LessThanOrEqualTo:
                            filterClause = "{0} <= " + param;
                            break;
                        case FilterOperator.GreaterThanOrEqualTo:
                            filterClause = "{0} >= " + param;
                            break;
                    }
                }

                if (string.IsNullOrWhiteSpace(filterClause))
                {
                    filterClause = "1 == 1";
                    iterateParam = false;
                }
                else
                {
                    filterClause = string.Format("({0})", string.Format(filterClause, filter.Field));
                }

                if (iterateParam)
                {
                    paramCount++;
                    values.Add(filter.GetTypedValue());
                }
            }

            return filterClause;
        }

        /// <summary>
        /// Applies the filters.
        /// </summary>
        /// <typeparam name="T">The type of the entity within the query.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="logic">The logic.</param>
        /// <param name="filters">The filters.</param>
        /// <param name="paramCount">The parameter count.</param>
        /// <param name="values">The values.</param>
        /// <returns>The filter clause.</returns>
        private static string ApplyFilters<T>(this IEnumerable<T> query, FilterLogic logic, IEnumerable<FilterModel> filters, ref int paramCount, ref List<object> values)
        {
            string sep = logic == FilterLogic.And ? " && " : " || ";
            List<string> allFilterClauses = new List<string>();
            foreach (FilterModel f in filters)
            {
                allFilterClauses.Add(query.ApplySingleFilter(f, ref paramCount, ref values));
            }

            return string.Join(sep, allFilterClauses);
        }

        /// <summary>
        /// Gets the typed value.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The object with a value that is most closely matching the intended type or null if it does not.</returns>
        private static object GetTypedValue(this FilterModel filter)
        {
            string valueStr = (filter.Value ?? string.Empty).ToString();
            if (string.IsNullOrWhiteSpace(filter.ValueType))
            {
                return valueStr;
            }
            else if (filter.ValueType == "bool")
            {
                bool value;
                if (bool.TryParse(valueStr, out value))
                {
                    return value;
                }
                else
                {
                    return default(bool?);
                }
            }
            else if (filter.ValueType == "int")
            {
                int value;
                if (int.TryParse(valueStr, out value))
                {
                    return value;
                }
                else
                {
                    return default(int?);
                }
            }
            else if (filter.ValueType.In("datetime", "date"))
            {
                DateTime value;
                if (DateTime.TryParse(valueStr, out value))
                {
                    return value;
                }
                else
                {
                    return default(DateTime?);
                }
            }
            else if (filter.ValueType.In("double", "decimal", "float", "number", "money"))
            {
                double value;
                if (double.TryParse(valueStr, out value))
                {
                    return value;
                }
                else
                {
                    return default(double?);
                }
            }
            else
            {
                return valueStr;
            }
        }
    }
}