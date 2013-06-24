namespace Spring.FluentContext.UnitTests.TestTypes
{
	public abstract class TypeWithProtectedFactoryMethod
	{
		public int GetValue()
		{
			return CreateType().CurrentCount;
		}

		protected abstract CountingType CreateType();
	}
}