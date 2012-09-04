using System;
using System.Linq.Expressions;
using Spring.Objects.Factory.Support;
using Spring.Objects.Factory.Config;

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

		public IObjectDefinitionBuilder<TObject> Autowire()
		{
			return Autowire(AutoWiringMode.AutoDetect);
		}

		public IObjectDefinitionBuilder<TObject> Autowire(AutoWiringMode mode)
		{
			_definition.AutowireMode = mode;
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

		public ILookupMethodDefinitionBuilder<TObject, TResult> BindLookupMethod<TResult>(Expression<Func<TObject, TResult>> methodSelector)
		{
			return new LookupMethodDefinitionBuilder<TObject, TResult>(this, ReflectionUtils.GetMethodName(methodSelector));
		}

		public ILookupMethodDefinitionBuilder<TObject, TResult> BindLookupMethodByName<TResult>(string methodName)
		{
			return new LookupMethodDefinitionBuilder<TObject, TResult>(this, methodName);
		}

		private void SetObjectType()
		{
			_definition.ObjectType = typeof(TObject);
		}
	}
}