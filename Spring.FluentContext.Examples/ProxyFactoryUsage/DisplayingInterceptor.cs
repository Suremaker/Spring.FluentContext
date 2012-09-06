using System;
using AopAlliance.Intercept;

namespace Spring.FluentContext.Examples.ProxyFactoryUsage
{
	class DisplayingInterceptor : IMethodInterceptor
	{
		public object Invoke(IMethodInvocation invocation)
		{
			Console.WriteLine("Calling {0}({1}) method...", 
			                  invocation.Method.Name,
			                  string.Join(", ", invocation.Arguments));

			return invocation.Proceed();
		}
	}
}