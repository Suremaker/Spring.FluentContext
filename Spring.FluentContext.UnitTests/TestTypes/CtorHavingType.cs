namespace Spring.FluentContext.UnitTests.TestTypes
{
	class CtorHavingType
	{
		public NestingType Nesting { get; private set; }

		public string Text { get; private set; }

		public int Value { get; private set; }

		public double OtherValue { get; private set; }

		public CtorHavingType(string text)
			: this(null, text, 0)
		{
		}

		public CtorHavingType(string text, int value)
			: this(null, text, value)
		{
		}

		public CtorHavingType(NestingType nesting)
			: this(nesting, null, 0)
		{
		}

		public CtorHavingType(NestingType nesting, string text, int value)
			: this(nesting, text, value, 0.0)
		{
		}

		public CtorHavingType(NestingType nesting, string text, int value, double otherValue)
		{
			Nesting = nesting;
			Text = text;
			Value = value;
			OtherValue = otherValue;
		}
	}
}