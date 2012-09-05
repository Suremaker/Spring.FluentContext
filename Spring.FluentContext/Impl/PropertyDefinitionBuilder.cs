using System;
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

		public IObjectDefinitionBuilder<TObject> ToValue(TProperty value)
		{
			return AddPropertyValue(new PropertyValue(_propertyName, value));
		}

		public IObjectDefinitionBuilder<TObject> ToReference(string objectId)
		{
			return AddPropertyValue(new PropertyValue(_propertyName, new RuntimeObjectReference(objectId)));
		}

		public IObjectDefinitionBuilder<TObject> ToDefaultReference()
		{
			return ToReference(IdGenerator<TProperty>.GetDefaultId());
		}

		public IObjectDefinitionBuilder<TObject> ToInlineDefinition<TInnerObject>(Action<IObjectDefinitionBuilder<TInnerObject>> innerObjectBuildAction) where TInnerObject : TProperty
		{
			var builder = new ObjectDefinitionBuilder<TInnerObject>();
			innerObjectBuildAction(builder);
			return AddPropertyValue(new PropertyValue(_propertyName, builder.Definition));
		}

		private IObjectDefinitionBuilder<TObject> AddPropertyValue(PropertyValue propertyValue)
		{
			_builder.Definition.PropertyValues.Add(propertyValue);
			return _builder;
		}
	}
}