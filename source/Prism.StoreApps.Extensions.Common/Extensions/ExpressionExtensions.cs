using System;
using System.Linq.Expressions;

namespace Prism.StoreApps.Extensions.Common.Extensions
{
	public static class ExpressionExtensions
	{
		public static string GetPropertyName<T>(this object obj, Expression<Func<T>> property)
		{
			var lambda = (LambdaExpression)property;

			MemberExpression memberExpression;
			if (lambda.Body is UnaryExpression)
			{
				var unaryExpression = (UnaryExpression)lambda.Body;
				memberExpression = (MemberExpression)unaryExpression.Operand;
			}
			else
			{
				memberExpression = (MemberExpression)lambda.Body;
			}

			return memberExpression.Member.Name;
		}

		public static string GetPropertyName<TProp>(Expression<Func<TProp>> expression)
		{
			if (expression.Body.NodeType == ExpressionType.MemberAccess)
				return ((MemberExpression)expression.Body).Member.Name;

			if (expression.Body.NodeType == ExpressionType.Convert)
				return ((MemberExpression)((UnaryExpression)expression.Body).Operand).Member.Name;

			throw new ArgumentException(String.Format("Unable to get property name from expression '{0}'", expression), "expression");
		}

		public static string GetPropertyName<TObj>(Expression<Func<TObj, object>> expression)
		{
			if (expression.Body.NodeType == ExpressionType.MemberAccess)
				return ((MemberExpression)expression.Body).Member.Name;

			if (expression.Body.NodeType == ExpressionType.Convert)
				return ((MemberExpression)((UnaryExpression)expression.Body).Operand).Member.Name;
			
			throw new ArgumentException(String.Format("Unable to get property name from expression '{0}'", expression), "expression");
		}
	}
}
