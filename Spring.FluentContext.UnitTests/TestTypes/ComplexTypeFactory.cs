namespace Spring.FluentContext.UnitTests.TestTypes
{
	class ComplexTypeFactory
	{
		public const string DefaultInstanceText = "created by factory method";

		public string InstanceText { get; set; }

		public ComplexType Create()
		{
			return new ComplexType { Text = InstanceText };
		}

		public static ComplexType CreateInstance()
		{
			return new ComplexType { Text = DefaultInstanceText };
		}

		public ComplexType Create(string text)
		{
			return new ComplexType { Text = text };
		}

		public ComplexType Create(string text, string text2)
		{
			return new ComplexType { Text = text, Simple = new SimpleType { Text = text2 } };
		}

		public ComplexType Create(string text, string text2, int value)
		{
			return new ComplexType { Text = text, Simple = new SimpleType { Text = text2, Value = value } };
		}

		public static ComplexType CreateInstance(string text)
		{
			return new ComplexType { Text = text };
		}

		public static ComplexType CreateInstance(string text, string text2)
		{
			return new ComplexType { Text = text, Simple = new SimpleType { Text = text2 } };
		}

		public static ComplexType CreateInstance(string text, string text2, int value)
		{
			return new ComplexType { Text = text, Simple = new SimpleType { Text = text2, Value = value } };
		}
	}
}
