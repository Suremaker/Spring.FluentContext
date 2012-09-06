using System;
using Spring.Context;

namespace Spring.FluentContext.Examples.AdvancedPropertySetterInjection
{
	internal class AdvancedPropertySetterInjectionExample : Example
	{
		protected override IApplicationContext CreateContext()
		{
			FluentApplicationContext ctx = new FluentApplicationContext();

			ctx.RegisterDefault<BobEngineer>();

			//the factories are registered with ids to allow having multiple instances of same class in context
			ctx.RegisterNamed<Factory>("standardFactory") 
				.BindProperty(f => f.Engineer).ToRegisteredDefaultOfType<BobEngineer>()
				.BindProperty(f => f.Description).ToValue("Standard Factory");

			ctx.RegisterDefault<RobotEngineer>();

			ctx.RegisterNamed<Factory>("roboticFactory")
				.BindProperty(f => f.Engineer).ToRegisteredDefaultOfType<RobotEngineer>()
				.BindProperty(f => f.Description).ToValue("Robotic Super Factory");

			return ctx;
		}

		protected override void RunExample(IApplicationContext ctx)
		{
			IFactory factory = ctx.GetObject<IFactory>("standardFactory");
			IFactory roboticFactory = ctx.GetObject<IFactory>("roboticFactory");

			BuildHouseUsing(factory);
			BuildHouseUsing(roboticFactory);
		}

		private void BuildHouseUsing(IFactory factory)
		{
			Console.WriteLine("\nUsing {0} to build house", factory.Description);
			factory.BuildHouse();
		}
	}
}