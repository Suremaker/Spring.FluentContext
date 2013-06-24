namespace Spring.FluentContext.Examples.AdvancedPropertySetterInjection.Objects
{
	class Factory : IFactory
	{
		public IFactoryEngineer Engineer { get; set; }
		public string Description { get; set; }

		public void BuildHouse()
		{
			Engineer.Construct("walls");
			Engineer.Construct("roof");
			Engineer.Construct("windows");
			Engineer.Construct("doors");
		}
	}
}