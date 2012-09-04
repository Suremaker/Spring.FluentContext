using Spring.Context.Support;

namespace Spring.FluentContext
{
	public class FluentApplicationContext : GenericApplicationContext
	{
		public IObjectDefinitionBuilder<T> Register<T>(string name)
		{
			var builder = new ObjectDefinitionBuilder<T>();
			RegisterObjectDefinition(name, builder.Definition);
			return builder;
		}
	}
}