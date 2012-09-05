using System;
using System.Threading;
using Spring.Context;

namespace Spring.FluentContext.Examples
{
	interface IFactoryEngineer
	{
		void Construct(string item);
	}

	class RobotEngineer : IFactoryEngineer
	{
		public void Construct(string item)
		{
			Console.WriteLine("Robot RX-543 is proceeding construction of {0}.", item);
		}
	}

	class BobEngineer : IFactoryEngineer
	{
		public void Construct(string item)
		{
			Console.WriteLine("Hi, I am Bob. I am going to build nice {0} for you...", item);
			Thread.Sleep(250);
		}
	}

	interface IFactory
	{
		void BuildHouse();
		string Description { get; }
	}

	class Factory : IFactory
	{
		public IFactoryEngineer Engineer { get; set; }
		public string Description { get; set; }

		public void BuildHouse()
		{
			Engineer.Construct("walls");
			Engineer.Construct("roof");
			Engineer.Construct("windows");
			Engineer.Construct("doors");
		}
	}

	internal class AdvancedPropertySetterInjection : Example
	{
		protected override IApplicationContext CreateContext()
		{
			FluentApplicationContext ctx = new FluentApplicationContext();

			ctx.RegisterDefault<BobEngineer>();
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