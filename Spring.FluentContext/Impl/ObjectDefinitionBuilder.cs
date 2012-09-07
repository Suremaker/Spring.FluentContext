using System;
using System.Linq.Expressions;
using Spring.FluentContext.Builders;
using Spring.FluentContext.BuildingStages;
using Spring.FluentContext.Utils;
using Spring.Objects.Factory.Support;
using Spring.Objects.Factory.Config;

namespace Spring.FluentContext.Impl
{
	internal class ObjectDefinitionBuilder<TObject> : IDefinitionHolder, IScopeBuildStage<TObject>
	{
		private readonly GenericObjectDefinition _definition = new GenericObjectDefinition();
		private readonly ObjectRef<TObject> _ref;

		public ObjectDefinitionBuilder(string objectId)
		{
			_ref = new ObjectRef<TObject>(objectId);
			SetObjectType();
		}

		public GenericObjectDefinition Definition
		{
			get { return _definition; }
		}

		public IInstantiationBuildStage<TObject> AsPrototype()
		{
			_definition.IsSingleton = false;
			return this;
		}

		public IInstantiationBuildStage<TObject> AsSingleton()
		{
			_definition.IsSingleton = true;
			return this;
		}

		public IConfigurationBuildStage<TObject> Autowire()
		{
			return Autowire(AutoWiringMode.AutoDetect);
		}

		public IConfigurationBuildStage<TObject> Autowire(AutoWiringMode mode)
		{
			_definition.AutowireMode = mode;
			return this;
		}

		public IReferencingStage<TObject> CheckDependencies()
		{
			return CheckDependencies(DependencyCheckingMode.All);
		}

		public IReferencingStage<TObject> CheckDependencies(DependencyCheckingMode mode)
		{
			_definition.DependencyCheck = mode;
			return this;
		}

		public IPropertyDefinitionBuilder<TObject, TProperty> BindProperty<TProperty>(Expression<Func<TObject, TProperty>> propertySelector)
		{
			return new PropertyDefinitionBuilder<TObject, TProperty>(this, ReflectionUtils.GetPropertyName(propertySelector));
		}

		public IPropertyDefinitionBuilder<TObject, TProperty> BindPropertyNamed<TProperty>(string propertyName)
		{
			return new PropertyDefinitionBuilder<TObject, TProperty>(this, propertyName);
		}

		public ICtorArgumentDefinitionBuilder<IInstantiationBuildStage<TObject>, TProperty> BindConstructorArg<TProperty>(int argIndex)
		{
			return new CtorArgumentDefinitionBuilder<IInstantiationBuildStage<TObject>, TProperty>(this, this, argIndex);
		}

		public ICtorArgumentDefinitionBuilder<IInstantiationBuildStage<TObject>, TProperty> BindConstructorArg<TProperty>()
		{
			return new CtorArgumentDefinitionBuilder<IInstantiationBuildStage<TObject>, TProperty>(this, this);
		}

		public ICtorDefinitionBuilder<TObject,TArg> UseConstructor<TArg>(Func<TArg,TObject> constructorSelector)
		{
			return new CtorDefinitionBuilder<TObject, TArg>(this);
		}

		public ICtorDefinitionBuilder<TObject,TArg1,TArg2> UseConstructor<TArg1,TArg2>(Func<TArg1,TArg2,TObject> constructorSelector)
		{
			return new CtorDefinitionBuilder<TObject, TArg1, TArg2>(this);
		}

		public ICtorDefinitionBuilder<TObject,TArg1,TArg2,TArg3> UseConstructor<TArg1,TArg2,TArg3>(Func<TArg1,TArg2,TArg3,TObject> constructorSelector)
		{
			return new CtorDefinitionBuilder<TObject, TArg1, TArg2, TArg3>(this);
		}

		public ICtorDefinitionBuilder<TObject,TArg1,TArg2,TArg3,TArg4> UseConstructor<TArg1,TArg2,TArg3,TArg4>(Func<TArg1,TArg2,TArg3,TArg4,TObject> constructorSelector)
		{
			return new CtorDefinitionBuilder<TObject, TArg1, TArg2, TArg3, TArg4>(this);
		}

		public ILookupMethodDefinitionBuilder<TObject, TResult> BindLookupMethod<TResult>(Expression<Func<TObject, TResult>> methodSelector)
		{
			return new LookupMethodDefinitionBuilder<TObject, TResult>(this, ReflectionUtils.GetMethodName(methodSelector));
		}

		public ILookupMethodDefinitionBuilder<TObject, TResult> BindLookupMethodNamed<TResult>(string methodName)
		{
			return new LookupMethodDefinitionBuilder<TObject, TResult>(this, methodName);
		}

		public ObjectRef<TObject> GetReference()
		{
			return _ref;
		}

		private void SetObjectType()
		{
			_definition.ObjectType = typeof(TObject);
		}
	}
}