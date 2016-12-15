// ***********************************************************************
// <copyright file="ExpressionExtensions.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Linq.Expressions;

namespace Kore.Extensions
{
    /// <summary>
    /// Provides extensions for retrieving information from expressions.
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Gets the member information.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The expression member info.</returns>
        public static MemberExpression GetMemberInfo(this Expression method)
        {
            LambdaExpression lambda = method as LambdaExpression;
            if (lambda == null)
            {
                return null;
            }

            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
            {
                return null;
            }

            return memberExpr;
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The name of the property from the expression.</returns>
        public static string GetPropertyName(this Expression method)
        {
            MemberExpression exp = method.GetMemberInfo();
            if (exp == null)
            {
                return string.Empty;
            }
            else
            {
                return exp.Member.Name;
            }
        }
    }
}
