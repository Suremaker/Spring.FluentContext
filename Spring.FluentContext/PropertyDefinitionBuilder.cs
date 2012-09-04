using Spring.Objects;

namespace Spring.FluentContext
{
	public class PropertyDefinitionBuilder<TObject, TProperty> : IPropertyDefinitionBuilder<TObject, TProperty>
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

		private IObjectDefinitionBuilder<TObject> AddPropertyValue(PropertyValue propertyValue)
		{
			_builder.Definition.PropertyValues.Add(propertyValue);
			return _builder;
		}
	}
}