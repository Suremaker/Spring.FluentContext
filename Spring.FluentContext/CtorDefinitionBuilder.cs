using System;
using Spring.Objects.Factory.Config;

namespace Spring.FluentContext
{
	public class CtorDefinitionBuilder<TObject, TProperty> : ICtorDefinitionBuilder<TObject, TProperty>
	{
		private readonly ObjectDefinitionBuilder<TObject> _builder;
		private readonly Action<ConstructorArgumentValues, object> _action;

		public CtorDefinitionBuilder(ObjectDefinitionBuilder<TObject> builder, int argIndex)
		{
			_builder = builder;
			_action = (list, value) => list.AddIndexedArgumentValue(argIndex, value, typeof(TProperty).FullName);
		}

		public CtorDefinitionBuilder(ObjectDefinitionBuilder<TObject> builder)
		{
			_builder = builder;
			_action = (list, value) => list.AddGenericArgumentValue(value, typeof(TProperty).FullName);
		}

		public IObjectDefinitionBuilder<TObject> ToValue(TProperty value)
		{
			_action(_builder.Definition.ConstructorArgumentValues, value);
			return _builder;
		}

		public IObjectDefinitionBuilder<TObject> ToReference(string objectId)
		{
			_action(_builder.Definition.ConstructorArgumentValues, new RuntimeObjectReference(objectId));
			return _builder;
		}
	}
}