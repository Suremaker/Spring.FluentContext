using System;
using Spring.FluentContext.Builders;
using Spring.FluentContext.BuildingStages;
using Spring.FluentContext.Utils;
using Spring.Objects;
using Spring.Objects.Factory.Config;

namespace Spring.FluentContext.Impl
{
	internal class PropertyDefinitionBuilder<TObject, TProperty> : IPropertyDefinitionBuilder<TObject, TProperty>
	{
		private readonly ObjectDefinitionBuilder<TObject> _builder;
		private readonly string _propertyName;

		public PropertyDefinitionBuilder(ObjectDefinitionBuilder<TObject> builder, string propertyName)
		{
			_builder = builder;
			_propertyName = propertyName;
		}

		public IConfigurationBuildStage<TObject> ToValue(TProperty value)
		{
			return AddPropertyValue(new PropertyValue(_propertyName, value));
		}

		public IConfigurationBuildStage<TObject> ToRegistered(string objectId)
		{
			return AddPropertyValue(new PropertyValue(_propertyName, new RuntimeObjectReference(objectId)));
		}

		public IConfigurationBuildStage<TObject> ToRegistered<TRef>(ObjectRef<TRef> reference) where TRef : TProperty
		{
			return ToRegistered(reference.Id);
		}

		public IConfigurationBuildStage<TObject> ToRegisteredDefaultOfType<TReferencedType>() where TReferencedType : TProperty
		{
			return ToRegistered(IdGenerator<TReferencedType>.GetDefaultId());
		}

		public IConfigurationBuildStage<TObject> ToRegisteredDefault()
		{
			return ToRegistered(IdGenerator<TProperty>.GetDefaultId());
		}

		public IConfigurationBuildStage<TObject> ToInlineDefinition<TInnerObject>() where TInnerObject : TProperty
		{
			return AddPropertyValue(new PropertyValue(_propertyName, new ObjectDefinitionBuilder<TInnerObject>(null).Definition));
		}

		public IConfigurationBuildStage<TObject> ToInlineDefinition<TInnerObject>(Action<IInstantiationBuildStage<TInnerObject>> innerObjectBuildAction) where TInnerObject : TProperty
		{
			var builder = new ObjectDefinitionBuilder<TInnerObject>(null);
			innerObjectBuildAction(builder);
			return AddPropertyValue(new PropertyValue(_propertyName, builder.Definition));
		}

		private IConfigurationBuildStage<TObject> AddPropertyValue(PropertyValue propertyValue)
		{
			_builder.Definition.PropertyValues.Add(propertyValue);
			return _builder;
		}
	}
}