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

			ctx.RegisterDefault<DisplayCommand>()
				.AsPrototype(); //every request for DisplayCommand should return new instance

			ctx.RegisterDefault<RepeatingInterceptor>();
			ctx.RegisterDefault<DelayingInterceptor>()
				.BindConstructorArg<int>().ToValue(3000);

			ctx.RegisterDefaultProxyFactory<ICommand>()
				.TargetingDefaultOfType<DisplayCommand>()
				.ReturningPrototypes() //every request for ICommand should return new instance of proxy (comment it and type few lines during program run to see change)
				.AddInterceptorByDefaultReference<DelayingInterceptor>()
				.AddInterceptorByDefaultReference<RepeatingInterceptor>();

			ctx.RegisterDefault<Sender>().Autowire();
			ctx.RegisterDefault<Consumer>().Autowire()
				.BindLookupMethodNamed<ICommand>("GetCommand").ToRegisteredDefault(); //method is protected so it is not possible to use lambda to get it

			return ctx;
		}

		protected override void RunExample(IApplicationContext ctx)
		{
			var consumer = ctx.GetObject<Consumer>();
			var sender = ctx.GetObject<Sender>();
			consumer.Start();
			sender.Run();
			//to let background tasks to finish
			Thread.Sleep(3000);
		}
	}
}

