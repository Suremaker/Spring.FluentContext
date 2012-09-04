using Spring.Context.Support;
using Spring.Objects.Factory.Support;

namespace Spring.FluentContext
{
	public class FluentApplicationContext : GenericApplicationContext
	{
		public void Register<T>(string name)
		{
			RegisterObjectDefinition(name, new RootObjectDefinition { ObjectType = typeof(T) });
		}
	}
}