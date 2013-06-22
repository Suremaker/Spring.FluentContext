//
//  Author:
//    Wojciech Kotlarski
//
//  Copyright (c) 2012, Wojciech Kotlarski
//
//  All rights reserved.
//
//  Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.	 
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in
//       the documentation and/or other materials provided with the distribution.
//
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
//  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT 
//  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS 
//  FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR 
//  CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, 
//  EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, 
//  PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF 
//  LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
//  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
//  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//

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
		public static string GetPropertyName<TObject, TProperty>(Expression<Func<TObject, TProperty>> propertySelector)
		{
			return ((MemberExpression)propertySelector.Body).Member.Name;
		}

		public static string GetMethodName<TObject, TResult>(Expression<Func<TObject, TResult>> methodCallExpression)
		{
			return GetMethodName(GetMethodInfo(methodCallExpression));
		}

		public static string GetMethodName<TObject>(Expression<Action<TObject>> methodCallExpression)
		{
			return GetMethodName(GetMethodInfo(methodCallExpression));
		}

		public static MethodInfo GetMethodInfo<TObject, TResult>(Expression<Func<TObject, TResult>> methodCallExpression)
		{
			return ((MethodCallExpression)methodCallExpression.Body).Method;
		}

		public static MethodInfo GetMethodInfo<TObject>(Expression<Action<TObject>> methodCallExpression)
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

		public static string GetPropertyPath<TObject, TResult>(Expression<Func<TObject, TResult>> propertySelector)
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
