namespace Spring.FluentContext.Examples.LookupMethodInjection.Objects
{
	public interface ICreditsCalculator
	{
		bool IsAcceptable(params double[] points);
	}
}