namespace Spring.FluentContext.Examples.LookupMethodInjection
{
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
}