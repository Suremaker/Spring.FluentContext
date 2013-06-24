namespace Spring.FluentContext.Examples.CollectionInjection.Objects
{
	class Shop
	{
		private readonly string _name;

		public Shop(string name)
		{
			_name = name;
		}

		public override string ToString()
		{
			return _name;
		}
	}
}