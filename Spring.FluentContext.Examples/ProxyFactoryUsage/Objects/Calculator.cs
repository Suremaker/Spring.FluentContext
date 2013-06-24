namespace Spring.FluentContext.Examples.ProxyFactoryUsage.Objects
{
	class Calculator : ICalculator
	{
		public int Add(int x, int y)
		{
			return x + y;
		}
	}
}