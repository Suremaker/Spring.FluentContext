namespace Spring.FluentContext.UnitTests.TestTypes
{
	public class CountingType
	{
		public CountingType()
		{
			CurrentCount = ++Count;
		}

		public int CurrentCount { get; private set; }

		public static int Count { get; private set; }

		public static void ClearCounter()
		{
			Count = 0;
		}
	}
}