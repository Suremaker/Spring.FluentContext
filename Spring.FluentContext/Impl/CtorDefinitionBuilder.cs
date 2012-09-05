using System;
using Spring.FluentContext.Utils;
using Spring.Objects.Factory.Config;

namespace Spring.FluentContext.Impl
{
	internal class CtorDefinitionBuilder<TObject, TArgument> : ICtorDefinitionBuilder<TObject, TArgument>
	{
		private readonly ObjectDefinitionBuilder<TObject> _builder;
		private readonly Action<ConstructorArgumentValues, object> _insertCtorArgAction;

		public CtorDefinitionBuilder(ObjectDefinitionBuilder<TObject> builder, int argIndex)
		{
			_builder = builder;
			_insertCtorArgAction = (list, value) => list.AddIndexedArgumentValue(argIndex, value, typeof(TArgument).FullName);
		}

		public CtorDefinitionBuilder(ObjectDefinitionBuilder<TObject> builder)
		{
			_builder = builder;
			_insertCtorArgAction = (list, value) => list.AddGenericArgumentValue(value, typeof(TArgument).FullName);
		}

		public IObjectDefinitionBuilder<TObject> ToValue(TArgument value)
		{
			_insertCtorArgAction(_builder.Definition.ConstructorArgumentValues, value);
			return _builder;
		}

		public IObjectDefinitionBuilder<TObject> ToReference(string objectId)
		{
			_insertCtorArgAction(_builder.Definition.ConstructorArgumentValues, new RuntimeObjectReference(objectId));
			return _builder;
		}

		public IObjectDefinitionBuilder<TObject> ToReference<TRef>(ObjectRef<TRef> reference) where TRef : TArgument
		{
			return ToReference(reference.Id);
		}

		public IObjectDefinitionBuilder<TObject> ToDefaultReference()
		{
			return ToReference(IdGenerator<TArgument>.GetDefaultId());
		}

		public IObjectDefinitionBuilder<TObject> ToInlineDefinition<TInnerObject>(Action<IObjectDefinitionBuilder<TInnerObject>> innerObjectBuildAction) where TInnerObject : TArgument
		{
			var innerObjectBuilder = new ObjectDefinitionBuilder<TInnerObject>(null);
			innerObjectBuildAction(innerObjectBuilder);
			_insertCtorArgAction(_builder.Definition.ConstructorArgumentValues, innerObjectBuilder.Definition);
			return _builder;
		}
	}
}