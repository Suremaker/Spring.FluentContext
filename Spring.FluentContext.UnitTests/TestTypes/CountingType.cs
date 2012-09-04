namespace Spring.FluentContext.UnitTests.TestTypes
{
	public class CountingType
	{
		public CountingType()
		{
			++Count;
		}

		public static int Count { get; private set; }
	}
}