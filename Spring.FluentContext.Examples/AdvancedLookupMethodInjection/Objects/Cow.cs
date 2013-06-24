namespace Spring.FluentContext.Examples.AdvancedLookupMethodInjection.Objects
{
	class Cow : IAnimal
	{
		private static int _instanceCounter;
		private readonly int _number;

		public Cow()
		{
			_number = ++_instanceCounter;
		}

		public override string ToString()
		{
			return string.Format("{0} {1}", GetType().Name, _number);
		}
	}
}