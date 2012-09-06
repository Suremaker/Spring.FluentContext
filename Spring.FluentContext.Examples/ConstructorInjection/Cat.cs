namespace Spring.FluentContext.Examples.ConstructorInjection
{
	class Cat
	{
		private readonly string _name;

		public Cat(string name)
		{
			_name = name;
		}

		public override string ToString()
		{
			return _name;
		}
	}
}