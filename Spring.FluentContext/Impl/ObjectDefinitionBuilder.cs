//
//  Author:
//    Wojciech Kotlarski
//
//  Copyright (c) 2012, Wojciech Kotlarski
//
//  All rights reserved.
//
//  Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.	 
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in
//       the documentation and/or other materials provided with the distribution.
//
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
//  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT 
//  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS 
//  FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR 
//  CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, 
//  EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, 
//  PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF 
//  LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
//  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
//  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//

using System;
using System.Linq;
using System.Linq.Expressions;
using Spring.FluentContext.Builders;
using Spring.FluentContext.BuildingStages;
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

		public IExternalDependencyBuildStage<TObject> AsPrototype()
		{
			_definition.IsSingleton = false;
			return this;
		}

		public IExternalDependencyBuildStage<TObject> AsSingleton()
		{
			_definition.IsSingleton = true;
			return this;
		}

		public IExternalDependencyBuildStage<TObject> DependingOnDefault<TOtherObject>()
		{
			return DependingOn<TOtherObject>(IdGenerator<TOtherObject>.GetDefaultId());
		}

		public IExternalDependencyBuildStage<TObject> DependingOn<TOtherObject>(string objectId)
		{
			_definition.DependsOn = _definition.DependsOn.AsEnumerable().Union(Enumerable.Repeat(objectId, 1)).ToArray();
			return this;
		}

		public IExternalDependencyBuildStage<TObject> DependingOn<TOtherObject>(ObjectRef<TOtherObject> reference)
		{
			return DependingOn<TOtherObject>(reference.Id);
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

		public IAutoConfigurationBuildStage<TObject> UseStaticFactoryMethod(Func<TObject> factoryMethodSelector)
		{
			_definition.ObjectType = factoryMethodSelector.Method.DeclaringType;
			_definition.FactoryMethodName = factoryMethodSelector.Method.Name;
			return this;
		}

		public IFactoryMethodDefinitionBuilder<TFactory, TObject> UseFactoryMethod<TFactory>(Expression<Func<TFactory,TObject>> factoryMethodSelector)
		{
			_definition.ObjectType = typeof(TFactory);
			_definition.FactoryMethodName = ReflectionUtils.GetMethodName(factoryMethodSelector);
			return new FactoryMethodDefinitionBuilder<TFactory,TObject>(this);
		}

		public ICtorArgumentDefinitionBuilder<ILooseCtorDefinitionBuildStage<TObject>, TProperty> BindConstructorArg<TProperty>(int argIndex)
		{
			return new CtorArgumentDefinitionBuilder<ILooseCtorDefinitionBuildStage<TObject>, TProperty>(this, this, argIndex);
		}

		public ICtorArgumentDefinitionBuilder<ILooseCtorDefinitionBuildStage<TObject>, TProperty> BindConstructorArg<TProperty>()
		{
			return new CtorArgumentDefinitionBuilder<ILooseCtorDefinitionBuildStage<TObject>, TProperty>(this, this);
		}

		public ICtorDefinitionBuildStage<TObject,TArg> UseConstructor<TArg>(Func<TArg,TObject> constructorSelector)
		{
			return new CtorDefinitionBuilder<TObject, TArg>(this);
		}

		public ICtorDefinitionBuildStage<TObject,TArg1,TArg2> UseConstructor<TArg1,TArg2>(Func<TArg1,TArg2,TObject> constructorSelector)
		{
			return new CtorDefinitionBuilder<TObject, TArg1, TArg2>(this);
		}

		public ICtorDefinitionBuildStage<TObject,TArg1,TArg2,TArg3> UseConstructor<TArg1,TArg2,TArg3>(Func<TArg1,TArg2,TArg3,TObject> constructorSelector)
		{
			return new CtorDefinitionBuilder<TObject, TArg1, TArg2, TArg3>(this);
		}

		public ICtorDefinitionBuildStage<TObject,TArg1,TArg2,TArg3,TArg4> UseConstructor<TArg1,TArg2,TArg3,TArg4>(Func<TArg1,TArg2,TArg3,TArg4,TObject> constructorSelector)
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