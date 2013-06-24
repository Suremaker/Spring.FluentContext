using Spring.Objects.Factory.Support;

namespace Spring.FluentContext.Impl
{
	internal interface IDefinitionHolder
	{
		GenericObjectDefinition Definition { get; }
	}

}