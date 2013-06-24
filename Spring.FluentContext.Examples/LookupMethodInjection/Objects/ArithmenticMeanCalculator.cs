using System.Linq;

namespace Spring.FluentContext.Examples.LookupMethodInjection.Objects
{
	public class ArithmenticMeanCalculator : IMeanCalculator
	{
		public double Calculate(params double[] values)
		{
			return (values.Length == 0) ? 0 : values.Sum() / values.Length;
		}
	}
}