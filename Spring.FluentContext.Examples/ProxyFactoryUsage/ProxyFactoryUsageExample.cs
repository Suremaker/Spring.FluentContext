using System;
using Spring.Context;

namespace Spring.FluentContext.Examples.ProxyFactoryUsage
{
	internal class ProxyFactoryUsageExample : Example
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