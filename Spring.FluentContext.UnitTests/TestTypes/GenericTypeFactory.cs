namespace Spring.FluentContext.UnitTests.TestTypes
{
	class GenericTypeFactory
	{
		public T Create<T>() where T : new()
		{
			return new T();
		}

		public static T CreateInstance<T>() where T : new()
		{
			return new T();
		}
	}
}