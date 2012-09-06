namespace Spring.FluentContext.Examples.LookupMethodInjection
{
	public interface ICreditsCalculator
	{
		bool IsAcceptable(params double[] points);
	}
}