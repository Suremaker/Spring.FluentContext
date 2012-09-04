using System;
using System.Linq.Expressions;
using Spring.Objects.Factory.Config;
using Spring.Objects.Factory.Support;

namespace Spring.FluentContext
{
	public class ObjectDefinitionBuilder<TObject> : IObjectDefinitionBuilder<TObject>
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

		public IObjectDefinitionBuilder<TObject> AsPrototype()
		{
			_definition.IsSingleton = false;
			return this;
		}

		public IObjectDefinitionBuilder<TObject> AsSingleton()
		{
			_definition.IsSingleton = true;
			return this;
		}

		public IPropertyDefinitionBuilder<TObject, TProperty> BindProperty<TProperty>(Expression<Func<TObject, TProperty>> propertySelector)
		{
			return new PropertyDefinitionBuilder<TObject, TProperty>(this, ReflectionUtils.GetPropertyName(propertySelector));
		}

		public ICtorDefinitionBuilder<TObject, TProperty> BindConstructorArg<TProperty>(int argIndex)
		{
			return new CtorDefinitionBuilder<TObject, TProperty>(this, argIndex);
		}

		public ICtorDefinitionBuilder<TObject, TProperty> BindConstructorArg<TProperty>()
		{
			return new CtorDefinitionBuilder<TObject, TProperty>(this);
		}

		private void SetObjectType()
		{
			_definition.ObjectType = typeof(TObject);
		}
	}
}