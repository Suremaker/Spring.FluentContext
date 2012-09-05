using AopAlliance.Intercept;

namespace Spring.FluentContext.UnitTests.TestTypes
{
	class MultiplingInterceptor : IMethodInterceptor
	{
		public object Invoke(IMethodInvocation invocation)
		{
			var result = invocation.Proceed();
			return ((int)result) * 10;
		}
	}
}
