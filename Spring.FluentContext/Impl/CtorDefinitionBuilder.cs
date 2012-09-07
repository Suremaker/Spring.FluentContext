using System;
using Spring.FluentContext.Builders;
using Spring.FluentContext.BuildingStages;
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

		public IInstantiationBuildStage<TObject> ToValue(TArgument value)
		{
			_insertCtorArgAction(_builder.Definition.ConstructorArgumentValues, value);
			return _builder;
		}

		public IInstantiationBuildStage<TObject> ToRegistered(string objectId)
		{
			_insertCtorArgAction(_builder.Definition.ConstructorArgumentValues, new RuntimeObjectReference(objectId));
			return _builder;
		}

		public IInstantiationBuildStage<TObject> ToRegistered<TRef>(ObjectRef<TRef> reference) where TRef : TArgument
		{
			return ToRegistered(reference.Id);
		}

		public IInstantiationBuildStage<TObject> ToRegisteredDefault()
		{
			return ToRegistered(IdGenerator<TArgument>.GetDefaultId());
		}

		public IInstantiationBuildStage<TObject> ToRegisteredDefaultOfType<TReferencedType>() where TReferencedType : TArgument
		{
			return ToRegistered(IdGenerator<TReferencedType>.GetDefaultId());
		}

		public IInstantiationBuildStage<TObject> ToInlineDefinition<TInnerObject>() where TInnerObject : TArgument
		{
			_insertCtorArgAction(
				_builder.Definition.ConstructorArgumentValues,
				new ObjectDefinitionBuilder<TInnerObject>(null).Definition);

			return _builder;
		}

		public IInstantiationBuildStage<TObject> ToInlineDefinition<TInnerObject>(Action<IInstantiationBuildStage<TInnerObject>> innerObjectBuildAction) where TInnerObject : TArgument
		{
			var innerObjectBuilder = new ObjectDefinitionBuilder<TInnerObject>(null);
			innerObjectBuildAction(innerObjectBuilder);
			_insertCtorArgAction(_builder.Definition.ConstructorArgumentValues, innerObjectBuilder.Definition);
			return _builder;
		}
	}
}