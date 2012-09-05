using System;
using System.Linq;
using Spring.Context;

namespace Spring.FluentContext.Examples
{
	public interface IMeanCalculator
	{
		double Calculate(params double[] values);
	}

	public interface ICreditsCalculator
	{
		bool IsAcceptable(params double[] points);
	}

	public abstract class CreditsCalculator : ICreditsCalculator
	{
		private readonly double _minPointsValue;

		protected CreditsCalculator(double minPointsValue)
		{
			_minPointsValue = minPointsValue;
		}

		public bool IsAcceptable(params double[] points)
		{
			var mean = GetMeanCalculator().Calculate(points);
			return mean >= _minPointsValue;
		}

		public abstract IMeanCalculator GetMeanCalculator();
	}

	public class ArithmenticMeanCalculator : IMeanCalculator
	{
		public double Calculate(params double[] values)
		{
			return (values.Length == 0) ? 0 : values.Sum() / values.Length;
		}
	}

	internal class LookupMethodInjection : Example
	{
		protected override IApplicationContext CreateContext()
		{
			var ctx = new FluentApplicationContext();

			ctx.RegisterDefault<ArithmenticMeanCalculator>();
			ctx.RegisterNamed<CreditsCalculator>("calculator")
				.BindConstructorArg<double>().ToValue(2.5)
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