using AopAlliance.Intercept;

namespace Spring.FluentContext.Examples.Complex.Objects
{
	class RepeatingInterceptor : IMethodInterceptor
	{
		public object Invoke(IMethodInvocation invocation)
		{
			invocation.Proceed();
			return invocation.Proceed();
		}
	}
}