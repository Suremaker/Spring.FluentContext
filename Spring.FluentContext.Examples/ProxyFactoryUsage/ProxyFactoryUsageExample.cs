using System;
using Spring.Context;
using Spring.FluentContext.Examples.ProxyFactoryUsage.Objects;

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
				.TargetingDefault<Calculator>()
				.InterceptedByDefault<DisplayingInterceptor>();

			return ctx;
		}

		protected override void RunExample(IApplicationContext ctx)
		{
			Console.WriteLine("Result = {0}", ctx.GetObject<ICalculator>().Add(3, 5));
		}
	}
}