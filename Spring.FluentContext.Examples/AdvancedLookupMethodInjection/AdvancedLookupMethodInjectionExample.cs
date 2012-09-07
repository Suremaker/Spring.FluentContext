using System;
using Spring.Context;

namespace Spring.FluentContext.Examples.AdvancedLookupMethodInjection
{
	internal class AdvancedLookupMethodInjectionExample : Example
	{
		protected override IApplicationContext CreateContext()
		{
			var ctx = new FluentApplicationContext();

			ctx.RegisterDefault<Pig>()
				.AsPrototype() //every request will return a new instance of Pig
				.BindConstructorArg<string>().ToValue("Small Piggy");

			ctx.RegisterDefault<Cow>()
				.AsPrototype();

			ctx.RegisterDefault<Barn>()
				.AsPrototype();

			ctx.RegisterDefault<Pigstry>()
				.AsSingleton(); //The Pigstry is registered as singleton, so every request returns the same instance (what is visible on second RunFarm())

			ctx.RegisterNamed<Farm>("pigFarm")
				.BindLookupMethod(f => f.CreateAnimal()).ToRegisteredDefaultOfType<Pig>()
				.BindLookupMethod(f => f.CreateShelter()).ToRegisteredDefaultOfType<Pigstry>();

			ctx.RegisterNamed<Farm>("cowFarm")
				.BindLookupMethod(f => f.CreateAnimal()).ToRegisteredDefaultOfType<Cow>()
				.BindLookupMethod(f => f.CreateShelter()).ToRegisteredDefaultOfType<Barn>();

			return ctx;
		}

		protected override void RunExample(IApplicationContext ctx)
		{
			IFarm cowFarm = ctx.GetObject<IFarm>("cowFarm");
			IFarm pigFarm = ctx.GetObject<IFarm>("pigFarm");

			Console.WriteLine("\nRunning cow farm");
			cowFarm.RunFarm();

			Console.WriteLine("\nRunning pig farm");
			pigFarm.RunFarm();

			Console.WriteLine("\nRunning cow farm for second time");
			cowFarm.RunFarm();

			Console.WriteLine("\nRunning pig farm for second time");
			pigFarm.RunFarm();
		}
	}
}