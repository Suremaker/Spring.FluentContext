namespace Spring.FluentContext.UnitTests.TestTypes
{
	class CtorHavingType
	{
		public NestingType Nesting { get; private set; }

		public string Text { get; private set; }

		public int Value { get; private set; }

		public CtorHavingType(string text) : this(text, 0)
		{
		}

		public CtorHavingType(string text, int value)
		{
			Text = text;
			Value = value;
		}

		public CtorHavingType(NestingType nesting)
		{
			Nesting = nesting;
		}
	}
}