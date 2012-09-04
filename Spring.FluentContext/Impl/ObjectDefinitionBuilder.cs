using System;
using System.Linq.Expressions;
using Spring.Objects.Factory.Support;

namespace Spring.FluentContext.Impl
{
	internal class ObjectDefinitionBuilder<TObject> : IObjectDefinitionBuilder<TObject>
	{
		private readonly GenericObjectDefinition _definition = new GenericObjectDefinition();

		public ObjectDefinitionBuilder()
		{
			SetObjectType();
		}

		public GenericObjectDefinition Definition
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

		public IPropertyDefinitionBuilder<TObject, TProperty> BindPropertyByName<TProperty>(string propertyName)
		{
			return new PropertyDefinitionBuilder<TObject, TProperty>(this, propertyName);
		}

		public ICtorDefinitionBuilder<TObject, TProperty> BindConstructorArg<TProperty>(int argIndex)
		{
			return new CtorDefinitionBuilder<TObject, TProperty>(this, argIndex);
		}

		public ICtorDefinitionBuilder<TObject, TProperty> BindConstructorArg<TProperty>()
		{
			return new CtorDefinitionBuilder<TObject, TProperty>(this);
		}

		public IObjectDefinitionBuilder<TObject> BindLookupMethod<TResult>(Expression<Func<TObject, TResult>> methodSelector, string objectId)
		{
			_definition.MethodOverrides.Add(new LookupMethodOverride(ReflectionUtils.GetMethodName(methodSelector),objectId));
			return this;
		}

		public IObjectDefinitionBuilder<TObject> BindLookupMethodByName(string methodName, string objectId)
		{
			_definition.MethodOverrides.Add(new LookupMethodOverride(methodName, objectId));
			return this;
		}

		private void SetObjectType()
		{
			_definition.ObjectType = typeof(TObject);
		}
	}
}