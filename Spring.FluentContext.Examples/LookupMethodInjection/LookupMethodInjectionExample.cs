using System;
using System.Globalization;
using System.Linq;
using Spring.Context;
using Spring.FluentContext.Examples.LookupMethodInjection.Objects;

namespace Spring.FluentContext.Examples.LookupMethodInjection
{
	internal class LookupMethodInjectionExample : Example
	{
		protected override IApplicationContext CreateContext()
		{
			var ctx = new FluentApplicationContext();

			ctx.RegisterDefault<ArithmenticMeanCalculator>();

			ctx.RegisterDefault<CreditsCalculator>()
				.BindConstructorArg<double>().ToValue(2.5)
				//the line below instruct Spring to override GetMeanCalculator() method with one returning ArithmeticMeanCalculator instance registered above
				.BindLookupMethod(c => c.GetMeanCalculator()).ToRegisteredDefaultOf<ArithmenticMeanCalculator>();

			ctx.RegisterDefaultAlias<ICreditsCalculator>().ToRegisteredDefault<CreditsCalculator>();

			return ctx;
		}

		protected override void RunExample(IApplicationContext ctx)
		{
			var calc = ctx.GetDefaultObject<ICreditsCalculator>();
			CalculateCredits(calc, "Josh", 2.4, 4.3, 5.8);
			CalculateCredits(calc, "John", 2.4, 1.3, 3.2);
		}

		private void CalculateCredits(ICreditsCalculator calc, string person, params double[] points)
		{
			Console.WriteLine("{0} has {1} with his points: {2}",
				person,
				calc.IsAcceptable(points) ? "passed" : "NOT passed",
				string.Join(", ", points.Select(p => p.ToString(CultureInfo.InvariantCulture)).ToArray()));
		}
	}
}