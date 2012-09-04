namespace Spring.FluentContext.UnitTests.TestTypes
{
	public class CountingType
	{
		public CountingType()
		{
			++Count;
		}

		public static int Count { get; private set; }

		public static void ClearCounter()
		{
			Count = 0;
		}
	}
}