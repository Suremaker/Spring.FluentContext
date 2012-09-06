using System.Threading;
using Spring.Context;

namespace Spring.FluentContext.Examples.Complex
{
	class ComplexExample : Example
	{
		protected override IApplicationContext CreateContext()
		{
			var ctx = new FluentApplicationContext();
			ctx.RegisterDefault<Endpoint>();
			ctx.RegisterDefault<DisplayCommand>().AsPrototype();
			ctx.RegisterDefault<RepeatingInterceptor>();
			ctx.RegisterDefault<DelayingInterceptor>()
				.BindConstructorArg<int>().ToValue(3000);

			ctx.RegisterDefaultProxyFactory<ICommand>()
				.TargetingDefaultOfType<DisplayCommand>()
				.ReturningPrototypes()
				.AddInterceptorByDefaultReference<DelayingInterceptor>()
				.AddInterceptorByDefaultReference<RepeatingInterceptor>();

			ctx.RegisterDefault<Sender>().Autowire();
			ctx.RegisterDefault<Consumer>().Autowire()
				.BindLookupMethodNamed<ICommand>("GetCommand").ToRegisteredDefault();

			return ctx;
		}

		protected override void RunExample(IApplicationContext ctx)
		{
			var consumer = ctx.GetObject<Consumer>();
			var sender = ctx.GetObject<Sender>();
			consumer.Start();
			sender.Run();
			Thread.Sleep(3000);
		}
	}
}

