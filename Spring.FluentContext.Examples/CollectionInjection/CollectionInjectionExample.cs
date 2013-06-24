using System;
using System.Linq;
using Spring.Context;
using Spring.FluentContext.Definitions;
using Spring.FluentContext.Examples.CollectionInjection.Objects;

namespace Spring.FluentContext.Examples.CollectionInjection
{
	internal class CollectionInjectionExample : Example
	{
		protected override IApplicationContext CreateContext()
		{
			var ctx = new FluentApplicationContext();

			var painterRef = ctx.RegisterDefault<Painter>().GetReference();

			ctx.RegisterNamed<Polisher>("polisher");

			ctx.RegisterDefault<Distributor>()
				.BindProperty(d => d.DistributionMap).ToDictionary(dict =>
				{
					dict[Def.Value("Motorcycle")] = Def.Reference<Shop>();
					dict[Def.Value("Car")] = Def.Reference<Shop>();
					dict[Def.Value("Luxury Car")] = Def.Object<Shop>(s => s.BindConstructorArg<string>().ToValue("Luxury Store that nobody knows."));
				});

			ctx.RegisterDefault<Shop>()
				.UseConstructor<string>(name => new Shop(name))
				.BindConstructorArg().ToValue("Local store");

			ctx.RegisterDefault<Factory>()
				.UseConstructor<IWorker[]>(workers => new Factory(workers))
				.BindConstructorArg()
				.ToArray(
					Def.Object<Constructor>(),
					painterRef,
					Def.Reference<Polisher>("polisher"),
					Def.Reference<Distributor>()
				);

			return ctx;
		}

		protected override void RunExample(IApplicationContext ctx)
		{
			var factory = ctx.GetObject<Factory>();
			factory.Create("Car");
			factory.Create("Motorcycle");
			factory.Create("Luxury Car");

			var shopsInContext = ctx.GetObjectsOfType(typeof(Shop)).Values.OfType<Shop>().Select(s => s.ToString()).ToArray();
			Console.WriteLine("Shops available in context: {0}", string.Join(",", shopsInContext));
			Console.WriteLine("Done...");
		}
	}
}
