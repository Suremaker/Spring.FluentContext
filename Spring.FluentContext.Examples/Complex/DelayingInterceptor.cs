using System.Threading;
using AopAlliance.Intercept;

namespace Spring.FluentContext.Examples.Complex
{
	class DelayingInterceptor : IMethodInterceptor
	{
		private readonly int _time;

		public DelayingInterceptor(int time)
		{
			_time = time;
		}

		public object Invoke(IMethodInvocation invocation)
		{
			if(!invocation.Method.IsSpecialName)
				Thread.Sleep(_time);
			return invocation.Proceed();
		}
	}
}