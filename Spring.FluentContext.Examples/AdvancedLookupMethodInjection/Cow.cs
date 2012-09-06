namespace Spring.FluentContext.Examples.AdvancedLookupMethodInjection
{
	class Cow : IAnimal
	{
		public override string ToString()
		{
			return GetType().Name;
		}
	}
}