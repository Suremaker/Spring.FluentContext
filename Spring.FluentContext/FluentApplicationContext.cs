using Spring.Context.Support;
using Spring.FluentContext.Impl;
using Spring.FluentContext.Utils;

namespace Spring.FluentContext
{
	public class FluentApplicationContext : GenericApplicationContext
	{
		public IObjectDefinitionBuilder<T> Register<T>(string id)
		{
			var builder = new ObjectDefinitionBuilder<T>(id);
			RegisterObjectDefinition(id, builder.Definition);
			return builder;
		}

		public IObjectDefinitionBuilder<T> Register<T>()
		{
			string id = IdGenerator<T>.GetDefaultId();

			var builder = new ObjectDefinitionBuilder<T>(id);
			RegisterObjectDefinition(id, builder.Definition);

			return builder;
		}


	}
}