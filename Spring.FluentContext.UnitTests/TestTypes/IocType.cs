namespace Spring.FluentContext.UnitTests.TestTypes
{
	class IocType
	{
		public string Text { private get; set; }

		public override string ToString()
		{
			return Text;
		}
	}
}