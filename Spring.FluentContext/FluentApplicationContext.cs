using Spring.Context;
using Spring.Context.Support;
using Spring.FluentContext.Impl;
using Spring.FluentContext.Utils;
using Spring.Objects.Factory.Support;

namespace Spring.FluentContext
{
	public class FluentApplicationContext : GenericApplicationContext
	{
		public FluentApplicationContext() { }
		public FluentApplicationContext(bool caseSensitive) : base(caseSensitive) { }
		public FluentApplicationContext(DefaultListableObjectFactory objectFactory) : base(objectFactory) { }
		public FluentApplicationContext(IApplicationContext parent) : base(parent) { }
		public FluentApplicationContext(string name, bool caseSensitive, IApplicationContext parent) : base(name, caseSensitive, parent) { }
		public FluentApplicationContext(DefaultListableObjectFactory objectFactory, IApplicationContext parent) : base(objectFactory, parent) { }
		public FluentApplicationContext(string name, bool caseSensitive, IApplicationContext parent, DefaultListableObjectFactory objectFactory) : base(name, caseSensitive, parent, objectFactory) { }

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