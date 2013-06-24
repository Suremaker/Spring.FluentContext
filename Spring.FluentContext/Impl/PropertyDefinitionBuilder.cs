using System;
using Spring.FluentContext.Builders;
using Spring.FluentContext.BuildingStages.Objects;
using Spring.FluentContext.Definitions;
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

		public IObjectConfigurationBuildStage<TObject> ToValue(TProperty value)
		{
			return AddPropertyValue(new PropertyValue(_propertyName, value));
		}

		public IObjectConfigurationBuildStage<TObject> ToRegistered(string objectId)
		{
			return AddPropertyValue(new PropertyValue(_propertyName, new RuntimeObjectReference(objectId)));
		}

		public IObjectConfigurationBuildStage<TObject> ToRegistered(IObjectRef<TProperty> reference)
		{
			return ToRegistered(reference.Id);
		}

		public IObjectConfigurationBuildStage<TObject> ToRegisteredDefaultOf<TReferencedType>() where TReferencedType : TProperty
		{
			return ToRegistered(IdGenerator<TReferencedType>.GetDefaultId());
		}

		public IObjectConfigurationBuildStage<TObject> ToRegisteredDefault()
		{
			return ToRegistered(IdGenerator<TProperty>.GetDefaultId());
		}

		public IObjectConfigurationBuildStage<TObject> To(IDefinition<TProperty> definition)
		{
			return AddPropertyValue(new PropertyValue(_propertyName, definition.DefinitionObject));
		}

		public IObjectConfigurationBuildStage<TObject> ToInlineDefinition<TInnerObject>() where TInnerObject : TProperty
		{
			return AddPropertyValue(new PropertyValue(_propertyName, new ObjectDefinitionBuilder<TInnerObject>(null).Definition));
		}

		public IObjectConfigurationBuildStage<TObject> ToInlineDefinition<TInnerObject>(Action<IInstantiationBuildStage<TInnerObject>> innerObjectBuildAction) where TInnerObject : TProperty
		{
			var builder = new ObjectDefinitionBuilder<TInnerObject>(null);
			innerObjectBuildAction(builder);
			return AddPropertyValue(new PropertyValue(_propertyName, builder.Definition));
		}

		private IObjectConfigurationBuildStage<TObject> AddPropertyValue(PropertyValue propertyValue)
		{
			_builder.Definition.PropertyValues.Add(propertyValue);
			return _builder;
		}
	}
}