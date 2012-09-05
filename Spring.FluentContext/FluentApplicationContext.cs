using Spring.Context.Support;
using Spring.FluentContext.Impl;
using Spring.FluentContext.Utils;

namespace Spring.FluentContext
{
	public class FluentApplicationContext : GenericApplicationContext
	{
		public IObjectDefinitionBuilder<T> RegisterNamed<T>(string id)
		{
			var builder = new ObjectDefinitionBuilder<T>(id);
			RegisterObjectDefinition(id, builder.Definition);
			return builder;
		}

		public IObjectDefinitionBuilder<T> RegisterDefault<T>()
		{
			return RegisterNamed<T>(IdGenerator<T>.GetDefaultId());
		}

		public IObjectDefinitionBuilder<T> RegisterUniquelyNamed<T>()
		{
			return RegisterNamed<T>(IdGenerator<T>.GetUniqueId());
		}

		public IProxyFactoryDefinitionBuilder<T> RegisterNamedProxyFactory<T>(string id)
		{
			var builder = new ProxyFactoryDefinitionBuilder<T>(id);
			RegisterObjectDefinition(id, builder.Definition);
			return builder;
		}

		public IProxyFactoryDefinitionBuilder<T> RegisterDefaultProxyFactory<T>()
		{
			return RegisterNamedProxyFactory<T>(IdGenerator<T>.GetDefaultId());
		}

		public IProxyFactoryDefinitionBuilder<T> RegisterUniquelyNamedProxyFactory<T>()
		{
			return RegisterNamedProxyFactory<T>(IdGenerator<T>.GetUniqueId());
		}
	}
}