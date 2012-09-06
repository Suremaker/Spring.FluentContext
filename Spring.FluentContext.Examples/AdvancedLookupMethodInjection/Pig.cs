namespace Spring.FluentContext.Examples.AdvancedLookupMethodInjection
{
	class Pig : IAnimal
	{
		private readonly string _name;
		private static int _instanceCounter;
		private readonly int _number;

		public Pig(string name)
		{
			_name = name;
			_number = ++_instanceCounter;
		}

		public override string ToString()
		{
			return string.Format("{0} {1}", _name, _number);
		}
	}
}