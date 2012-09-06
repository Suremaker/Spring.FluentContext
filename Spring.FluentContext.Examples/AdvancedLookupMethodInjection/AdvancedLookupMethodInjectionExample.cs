using System;
using Spring.Context;

namespace Spring.FluentContext.Examples.AdvancedLookupMethodInjection
{
	internal class AdvancedLookupMethodInjectionExample : Example
	{
		protected override IApplicationContext CreateContext()
		{
			var ctx = new FluentApplicationContext();

			ctx.RegisterDefault<Pig>().AsPrototype();
			ctx.RegisterDefault<Cow>().AsPrototype();
			ctx.RegisterDefault<Pigstry>().AsSingleton();
			ctx.RegisterDefault<Barn>().AsPrototype();

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