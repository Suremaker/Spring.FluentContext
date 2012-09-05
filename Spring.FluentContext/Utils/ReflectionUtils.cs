using System;
using System.Linq.Expressions;

namespace Spring.FluentContext.Utils
{
	public static class ReflectionUtils
	{
		public static string GetPropertyName<TObject, TProperty>(Expression<Func<TObject, TProperty>> propertySelector)
		{
			return ((MemberExpression)propertySelector.Body).Member.Name;
		}

		public static string GetMethodName<TObject, TResult>(Expression<Func<TObject, TResult>> methodCallExpression)
		{
			return ((MethodCallExpression)methodCallExpression.Body).Method.Name;
		}

		public static string GetMethodName<TObject>(Expression<Action<TObject>> methodCallExpression)
		{
			return ((MethodCallExpression)methodCallExpression.Body).Method.Name;
		}
	}
}
