using System;
using System.Linq.Expressions;

namespace Spring.FluentContext
{
	public static class ReflectionUtils
	{
		public static string GetPropertyName<TObject, TProperty>(Expression<Func<TObject, TProperty>> propertySelector)
		{
			return ((MemberExpression)propertySelector.Body).Member.Name;
		}
	}
}
