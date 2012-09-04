using Spring.Context.Support;

namespace Spring.FluentContext
{
	public class FluentApplicationContext : GenericApplicationContext
	{
		public IObjectDefinitionBuilder<T> Register<T>(string id)
		{
			var builder = new ObjectDefinitionBuilder<T>();
			RegisterObjectDefinition(id, builder.Definition);
			return builder;
		}
	}
}