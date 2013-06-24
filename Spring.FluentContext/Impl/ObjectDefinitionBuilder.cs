using System;
using System.Linq;
using System.Linq.Expressions;
using Spring.FluentContext.Builders;
using Spring.FluentContext.BuildingStages;
using Spring.FluentContext.BuildingStages.Objects;
using Spring.FluentContext.Utils;
using Spring.Objects.Factory.Config;
using Spring.Objects.Factory.Support;

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

		public IIndirectDependencyBuildStage<TObject> AsPrototype()
		{
			_definition.IsSingleton = false;
			return this;
		}

		public IIndirectDependencyBuildStage<TObject> AsSingleton()
		{
			_definition.IsSingleton = true;
			return this;
		}

		public IIndirectDependencyBuildStage<TObject> DependingOnDefault<TOtherObject>()
		{
			return DependingOn<TOtherObject>(IdGenerator<TOtherObject>.GetDefaultId());
		}

		public IIndirectDependencyBuildStage<TObject> DependingOn<TOtherObject>(string objectId)
		{
			_definition.DependsOn = _definition.DependsOn.AsEnumerable().Union(Enumerable.Repeat(objectId, 1)).ToArray();
			return this;
		}

		public IIndirectDependencyBuildStage<TObject> DependingOn<TOtherObject>(IObjectRef<TOtherObject> reference)
		{
			return DependingOn<TOtherObject>(reference.Id);
		}

		public IObjectConfigurationBuildStage<TObject> Autowire()
		{
			return Autowire(AutoWiringMode.AutoDetect);
		}

		public IObjectConfigurationBuildStage<TObject> Autowire(AutoWiringMode mode)
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

		public IPropertyDefinitionBuilder<TObject, TProperty> BindProperty<TProperty>(
			Expression<Func<TObject, TProperty>> propertySelector)
		{
			return new PropertyDefinitionBuilder<TObject, TProperty>(this, ReflectionUtils.GetPropertyName(propertySelector));
		}

		public IPropertyDefinitionBuilder<TObject, TProperty> BindPropertyNamed<TProperty>(string propertyName)
		{
			return new PropertyDefinitionBuilder<TObject, TProperty>(this, propertyName);
		}

		public IAutoConfigurationBuildStage<TObject> UseStaticFactoryMethod(Func<TObject> factoryMethodSelector)
		{
			_definition.ObjectType = factoryMethodSelector.Method.DeclaringType;
			_definition.FactoryMethodName = ReflectionUtils.GetMethodName(factoryMethodSelector.Method);
			return this;
		}

		public IFactoryMethodArgBuildStage<TObject, TArg1> UseStaticFactoryMethod<TArg1>(Expression<Func<TArg1, TObject>> factoryMethodSelector)
		{
			var methodInfo = ReflectionUtils.GetMethodInfo(factoryMethodSelector);
			_definition.ObjectType = methodInfo.DeclaringType;
			_definition.FactoryMethodName = ReflectionUtils.GetMethodName(methodInfo);
			return new FactoryMethodArgDefinitionBuilder<TObject, TArg1>(this);
		}

		public IFactoryMethodArgBuildStage<TObject, TArg1, TArg2> UseStaticFactoryMethod<TArg1, TArg2>(Expression<Func<TArg1, TArg2, TObject>> factoryMethodSelector)
		{
			var methodInfo = ReflectionUtils.GetMethodInfo(factoryMethodSelector);
			_definition.ObjectType = methodInfo.DeclaringType;
			_definition.FactoryMethodName = ReflectionUtils.GetMethodName(methodInfo);
			return new FactoryMethodArgDefinitionBuilder<TObject, TArg1, TArg2>(this);
		}

		public IFactoryMethodArgBuildStage<TObject, TArg1, TArg2, TArg3> UseStaticFactoryMethod<TArg1, TArg2, TArg3>(Expression<Func<TArg1, TArg2, TArg3, TObject>> factoryMethodSelector)
		{
			var methodInfo = ReflectionUtils.GetMethodInfo(factoryMethodSelector);
			_definition.ObjectType = methodInfo.DeclaringType;
			_definition.FactoryMethodName = ReflectionUtils.GetMethodName(methodInfo);
			return new FactoryMethodArgDefinitionBuilder<TObject, TArg1, TArg2, TArg3>(this);
		}

		public IFactoryMethodDefinitionBuilder<TFactoryObject, TObject> UseFactoryMethod<TFactoryObject>(Expression<Func<TFactoryObject, TObject>> factoryMethodSelector)
		{
			_definition.ObjectType = typeof(TFactoryObject);
			_definition.FactoryMethodName = ReflectionUtils.GetMethodName(factoryMethodSelector);
			return new FactoryMethodDefinitionBuilder<TFactoryObject, TObject>(this);
		}

		public IFactoryMethodDefinitionBuilder<TFactoryObject, TObject, TArg1> UseFactoryMethod<TFactoryObject, TArg1>(
			Expression<Func<TFactoryObject, TArg1, TObject>> factoryMethodSelector)
		{
			_definition.ObjectType = typeof(TFactoryObject);
			_definition.FactoryMethodName = ReflectionUtils.GetMethodName(factoryMethodSelector);
			return new FactoryMethodDefinitionBuilder<TFactoryObject, TObject, TArg1>(this);
		}

		public IFactoryMethodDefinitionBuilder<TFactoryObject, TObject, TArg1, TArg2> UseFactoryMethod
			<TFactoryObject, TArg1, TArg2>(Expression<Func<TFactoryObject, TArg1, TArg2, TObject>> factoryMethodSelector)
		{
			_definition.ObjectType = typeof(TFactoryObject);
			_definition.FactoryMethodName = ReflectionUtils.GetMethodName(factoryMethodSelector);
			return new FactoryMethodDefinitionBuilder<TFactoryObject, TObject, TArg1, TArg2>(this);
		}

		public IFactoryMethodDefinitionBuilder<TFactoryObject, TObject, TArg1, TArg2, TArg3> UseFactoryMethod
			<TFactoryObject, TArg1, TArg2, TArg3>(
			Expression<Func<TFactoryObject, TArg1, TArg2, TArg3, TObject>> factoryMethodSelector)
		{
			_definition.ObjectType = typeof(TFactoryObject);
			_definition.FactoryMethodName = ReflectionUtils.GetMethodName(factoryMethodSelector);
			return new FactoryMethodDefinitionBuilder<TFactoryObject, TObject, TArg1, TArg2, TArg3>(this);
		}

		public IMethodArgumentDefinitionBuilder<ILooseCtorDefinitionBuildStage<TObject>, TProperty> BindConstructorArg<TProperty>(int argIndex)
		{
			return new MethodArgumentDefinitionBuilder<ILooseCtorDefinitionBuildStage<TObject>, TProperty>(this, this, argIndex);
		}

		public IMethodArgumentDefinitionBuilder<ILooseCtorDefinitionBuildStage<TObject>, TProperty> BindConstructorArg<TProperty>()
		{
			return new MethodArgumentDefinitionBuilder<ILooseCtorDefinitionBuildStage<TObject>, TProperty>(this, this);
		}

		public ICtorDefinitionBuildStage<TObject, TArg> UseConstructor<TArg>(Func<TArg, TObject> constructorSelector)
		{
			return new CtorDefinitionBuilder<TObject, TArg>(this);
		}

		public ICtorDefinitionBuildStage<TObject, TArg1, TArg2> UseConstructor<TArg1, TArg2>(Func<TArg1, TArg2, TObject> constructorSelector)
		{
			return new CtorDefinitionBuilder<TObject, TArg1, TArg2>(this);
		}

		public ICtorDefinitionBuildStage<TObject, TArg1, TArg2, TArg3> UseConstructor<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, TObject> constructorSelector)
		{
			return new CtorDefinitionBuilder<TObject, TArg1, TArg2, TArg3>(this);
		}

		public ICtorDefinitionBuildStage<TObject, TArg1, TArg2, TArg3, TArg4> UseConstructor<TArg1, TArg2, TArg3, TArg4>(Func<TArg1, TArg2, TArg3, TArg4, TObject> constructorSelector)
		{
			return new CtorDefinitionBuilder<TObject, TArg1, TArg2, TArg3, TArg4>(this);
		}

		public ILookupMethodDefinitionBuilder<TObject, TResult> BindLookupMethod<TResult>(Expression<Func<TObject, TResult>> methodSelector)
		{
			var methodInfo = ReflectionUtils.GetMethodInfo(methodSelector);

			if (methodInfo.IsGenericMethod)
				throw new InvalidOperationException(
					string.Format(
						"Lookup method binding for {0}() in object named '{1}' is not supported, because target method is generic.",
						ReflectionUtils.GetMethodName(methodInfo),
						IdGenerator<TObject>.GetDefaultId()));

			return new LookupMethodDefinitionBuilder<TObject, TResult>(this, methodInfo.Name);
		}

		public ILookupMethodDefinitionBuilder<TObject, TResult> BindLookupMethodNamed<TResult>(string methodName)
		{
			return new LookupMethodDefinitionBuilder<TObject, TResult>(this, methodName);
		}

		public IDestroyBehaviorBuildStage<TObject> CallOnInit(Expression<Action<TObject>> initMethodSelector)
		{
			_definition.InitMethodName = ReflectionUtils.GetMethodName(initMethodSelector);
			return this;
		}

		public IValidationBuildStage<TObject> CallOnDestroy(Expression<Action<TObject>> destroyMethodSelector)
		{
			_definition.DestroyMethodName = ReflectionUtils.GetMethodName(destroyMethodSelector);
			return this;
		}

		public IObjectRef<TObject> GetReference()
		{
			return _ref;
		}

		private void SetObjectType()
		{
			_definition.ObjectType = typeof(TObject);
		}
	}
}