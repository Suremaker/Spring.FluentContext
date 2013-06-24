using Spring.FluentContext.Definitions;

namespace Spring.FluentContext.Impl
{
	internal class Definition<T> : IDefinition<T>
	{
		public Definition(object value)
		{
			DefinitionObject = value;
		}

		public object DefinitionObject { get; private set; }
	}
}
