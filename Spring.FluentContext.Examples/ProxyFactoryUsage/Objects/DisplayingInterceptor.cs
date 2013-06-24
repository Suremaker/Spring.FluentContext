using System;
using System.Linq;
using AopAlliance.Intercept;

namespace Spring.FluentContext.Examples.ProxyFactoryUsage.Objects
{
	class DisplayingInterceptor : IMethodInterceptor
	{
		public object Invoke(IMethodInvocation invocation)
		{
			Console.WriteLine("Calling {0}({1}) method...",
							  invocation.Method.Name,
							  string.Join(", ", invocation.Arguments.Select(a => a.ToString()).ToArray()));

			return invocation.Proceed();
		}
	}
}