namespace Spring.FluentContext.Examples.AdvancedPropertySetterInjection
{
	interface IFactory
	{
		void BuildHouse();
		string Description { get; }
	}
}