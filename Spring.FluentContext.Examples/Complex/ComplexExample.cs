using System.Threading;
using Spring.Context;
using Spring.FluentContext.Examples.Complex.Objects;

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
				.TargetingDefault<DisplayCommand>()
				.ReturningPrototypes() //every request for ICommand should return new instance of proxy (comment it and type few lines during program run to see change)
				.InterceptedByDefault<DelayingInterceptor>()
				.InterceptedByDefault<RepeatingInterceptor>(); //Repeating interceptor is called after DelyingInterceptor, so there would be no delays between repeats

			ctx.RegisterDefault<Sender>()
				.DependingOnDefault<Consumer>()
				.Autowire(); //autowiring endpoint dependency

			ctx.RegisterDefault<Consumer>()
				.BindLookupMethodNamed<ICommand>("GetCommand").ToRegisteredDefault() //method is protected so it is not possible to use lambda to get it
				.Autowire() //autowiring endpoint dependency				
				.CallOnInit(c => c.Start());

			return ctx;
		}

		protected override void RunExample(IApplicationContext ctx)
		{
			var sender = ctx.GetObject<Sender>();
			sender.Run();
			//to let background tasks to finish
			Thread.Sleep(3000);
		}
	}
}

