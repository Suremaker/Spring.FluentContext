using System;
using AopAlliance.Intercept;
using Spring.Context;

namespace Spring.FluentContext.Examples
{
	public interface ICalculator
	{
		int Add(int x, int y);
	}

	class Calculator : ICalculator
	{
		public int Add(int x, int y)
		{
			return x + y;
		}
	}

	class DisplayingInterceptor : IMethodInterceptor
	{
		public object Invoke(IMethodInvocation invocation)
		{
			Console.WriteLine("Calling {0}({1}) method...", invocation.Method.Name,
				string.Join(", ", invocation.Arguments));
			return invocation.Proceed();
		}
	}

	internal class ProxyFactoryUsage : Example
	{
		protected override IApplicationContext CreateContext()
		{
			var ctx = new FluentApplicationContext();

			ctx.RegisterDefault<Calculator>();
			ctx.RegisterDefault<DisplayingInterceptor>();

			ctx.RegisterDefaultProxyFactory<ICalculator>()
				.TargetingDefaultOfType<Calculator>()
				.AddInterceptorByDefaultReference<DisplayingInterceptor>();

			return ctx;
		}

		protected override void RunExample(IApplicationContext ctx)
		{
			Console.WriteLine("Result = {0}", ctx.GetObject<ICalculator>().Add(3, 5));
		}
	}
}