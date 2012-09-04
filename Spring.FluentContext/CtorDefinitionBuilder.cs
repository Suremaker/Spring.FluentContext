using System;
using Spring.Objects.Factory.Config;

namespace Spring.FluentContext
{
	public class CtorDefinitionBuilder<TObject, TProperty> : ICtorDefinitionBuilder<TObject, TProperty>
	{
		private readonly ObjectDefinitionBuilder<TObject> _builder;
		private readonly Action<ConstructorArgumentValues, object> _insertCtorArgAction;

		public CtorDefinitionBuilder(ObjectDefinitionBuilder<TObject> builder, int argIndex)
		{
			_builder = builder;
			_insertCtorArgAction = (list, value) => list.AddIndexedArgumentValue(argIndex, value, typeof(TProperty).FullName);
		}

		public CtorDefinitionBuilder(ObjectDefinitionBuilder<TObject> builder)
		{
			_builder = builder;
			_insertCtorArgAction = (list, value) => list.AddGenericArgumentValue(value, typeof(TProperty).FullName);
		}

		public IObjectDefinitionBuilder<TObject> ToValue(TProperty value)
		{
			_insertCtorArgAction(_builder.Definition.ConstructorArgumentValues, value);
			return _builder;
		}

		public IObjectDefinitionBuilder<TObject> ToReference(string objectId)
		{
			_insertCtorArgAction(_builder.Definition.ConstructorArgumentValues, new RuntimeObjectReference(objectId));
			return _builder;
		}

		public IObjectDefinitionBuilder<TObject> ToInlineDefinition<TInnerObject>(Action<IObjectDefinitionBuilder<TInnerObject>> innerObjectBuildAction) where TInnerObject : TProperty
		{
			var innerObjectBuilder = new ObjectDefinitionBuilder<TInnerObject>();
			innerObjectBuildAction(innerObjectBuilder);
			_insertCtorArgAction(_builder.Definition.ConstructorArgumentValues, innerObjectBuilder.Definition);
			return _builder;
		}
	}
}