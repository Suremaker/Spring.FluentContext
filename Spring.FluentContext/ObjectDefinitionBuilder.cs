using Spring.Objects.Factory.Config;
using Spring.Objects.Factory.Support;

namespace Spring.FluentContext
{
	public class ObjectDefinitionBuilder<T> : IObjectDefinitionBuilder<T>
	{
		private readonly GenericObjectDefinition _definition = new GenericObjectDefinition();

		public ObjectDefinitionBuilder()
		{
			SetObjectType();
		}

		public IObjectDefinition Definition
		{
			get { return _definition; }
		}

		public IObjectDefinitionBuilder<T> AsPrototype()
		{
			_definition.IsSingleton = false;
			return this;
		}

		public IObjectDefinitionBuilder<T> AsSingleton()
		{
			_definition.IsSingleton = true;
			return this;
		}

		private void SetObjectType()
		{
			_definition.ObjectType = typeof(T);
		}
	}
}