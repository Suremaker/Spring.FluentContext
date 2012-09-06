using System;
using Spring.Context;

namespace Spring.FluentContext.Examples.LookupMethodInjection
{
	internal class LookupMethodInjectionExample : Example
	{
		protected override IApplicationContext CreateContext()
		{
			var ctx = new FluentApplicationContext();

			ctx.RegisterDefault<ArithmenticMeanCalculator>();

			ctx.RegisterNamed<CreditsCalculator>("calculator")
				.BindConstructorArg<double>().ToValue(2.5)
				//the line below instruct Spring to override GetMeanCalculator() method with one returning ArithmeticMeanCalculator instance registered above
				.BindLookupMethod(c => c.GetMeanCalculator()).ToRegisteredDefaultOfType<ArithmenticMeanCalculator>();
			return ctx;
		}

		protected override void RunExample(IApplicationContext ctx)
		{
			var calc = ctx.GetObject<ICreditsCalculator>("calculator");
			CalculateCredits(calc, "Josh", 2.4, 4.3, 5.8);
			CalculateCredits(calc, "John", 2.4, 1.3, 3.2);
		}

		private void CalculateCredits(ICreditsCalculator calc, string person, params double[] points)
		{
			Console.WriteLine("{0} has {1} with his points: {2}",
				person,
				calc.IsAcceptable(points) ? "passed" : "NOT passed",
				string.Join(", ", points));

		}
	}
}