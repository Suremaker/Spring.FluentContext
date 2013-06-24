using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Spring.FluentContext.Utils
{
	internal static class ReflectionUtils
	{
		public static string GetPropertyName<T>(Expression<T> propertySelector)
		{
			return ((MemberExpression)propertySelector.Body).Member.Name;
		}

		public static string GetMethodName<T>(Expression<T> methodCallExpression)
		{
			return GetMethodName(GetMethodInfo(methodCallExpression));
		}

		public static MethodInfo GetMethodInfo<T>(Expression<T> methodCallExpression)
		{
			return ((MethodCallExpression)methodCallExpression.Body).Method;
		}

		public static string GetMethodName(MethodInfo methodInfo)
		{
			if (!methodInfo.IsGenericMethod)
				return methodInfo.Name;
			return string.Format("{0}<{1}>", methodInfo.Name, string.Join(",", methodInfo.GetGenericArguments().Select(GetTypeFullName).ToArray()));
		}

		public static string GetTypeFullName(Type type)
		{
			if (!type.IsGenericType)
				return type.FullName;

			var sb = new StringBuilder();

			if (type.Namespace != null)
				sb.Append(type.Namespace).Append('.');

			sb.AppendFormat("{0}<{1}>", type.Name.Substring(0, type.Name.IndexOf('`')), string.Join(",", type.GetGenericArguments().Select(GetTypeFullName).ToArray()));
			return sb.ToString();
		}

		public static string GetPropertyPath<T>(Expression<T> propertySelector)
		{
			var path = new LinkedList<string>();

			var expression = propertySelector.Body;
			while (expression != null && expression.NodeType != ExpressionType.Parameter)
			{
				var memberExpression = expression as MemberExpression;
				if (memberExpression == null || !(memberExpression.Member is PropertyInfo))
					throw new ArgumentException("Lambda expression can contain only property access expressions.", "propertySelector");

				path.AddFirst(memberExpression.Member.Name);
				expression = memberExpression.Expression;
			}
			return string.Join(".", path.ToArray());
		}
	}
}
