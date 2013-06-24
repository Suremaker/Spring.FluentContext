namespace Spring.FluentContext.Examples.AdvancedPropertySetterInjection.Objects
{
	interface IFactory
	{
		void BuildHouse();
		string Description { get; }
	}
}