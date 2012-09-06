namespace Spring.FluentContext.Examples.AdvancedLookupMethodInjection
{
	class Pig : IAnimal
	{
		public override string ToString()
		{
			return GetType().Name;
		}
	}
}