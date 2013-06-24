using System.Collections.Generic;
using AopAlliance.Aop;
using Spring.Aop.Framework;
using Spring.FluentContext.BuildingStages.ProxyFactories;
using Spring.FluentContext.Utils;
using Spring.Objects.Factory.Config;

namespace Spring.FluentContext.Impl
{
	internal class ProxyFactoryDefinitionBuilder<TObject> : IProxyTargetDefinitionBuildStage<TObject>, IProxyInstantiationDefinitionBuildStage<TObject>
	{
		private readonly ObjectDefinitionBuilder<ProxyFactoryObject> _builder;
		private readonly List<string> _interceptorNames = new List<string>();
		private readonly ObjectRef<TObject> _ref;

		public ProxyFactoryDefinitionBuilder(string id)
		{
			_builder = new ObjectDefinitionBuilder<ProxyFactoryObject>(id);
			_ref = new ObjectRef<TObject>(id);
			SetTargetInterfaces();
		}

		private void SetTargetInterfaces()
		{
			_builder.BindProperty(f => f.Interfaces).ToValue(new[] { typeof(TObject) });
		}

		public IObjectDefinition Definition
		{
			get { return _builder.Definition; }
		}

		public IProxyInstantiationDefinitionBuildStage<TObject> Targeting(string objectId)
		{
			_builder.BindPropertyNamed<string>("TargetName").ToValue(objectId);
			return this;
		}

		public IProxyInstantiationDefinitionBuildStage<TObject> TargetingDefault<TReferencedType>() where TReferencedType : TObject
		{
			return Targeting(IdGenerator<TReferencedType>.GetDefaultId());
		}

		public IProxyInstantiationDefinitionBuildStage<TObject> Targeting(IObjectRef<TObject> reference)
		{
			return Targeting(reference.Id);
		}

		public IProxyInterceptorDefinitionBuildStage<TObject> InterceptedBy(string objectId)
		{
			_interceptorNames.Add(objectId);
			_builder.BindPropertyNamed<string[]>("InterceptorNames").ToValue(_interceptorNames.ToArray());
			return this;
		}

        public IProxyInterceptorDefinitionBuildStage<TObject> InterceptedBy<TInterceptorType>(IObjectRef<TInterceptorType> reference) where TInterceptorType : IAdvice
		{
			return InterceptedBy(reference.Id);
		}

        public IProxyInterceptorDefinitionBuildStage<TObject> InterceptedByDefault<TInterceptorType>() where TInterceptorType : IAdvice
		{
			return InterceptedBy(IdGenerator<TInterceptorType>.GetDefaultId());
		}

		public IObjectRef<TObject> GetReference()
		{
			return _ref;
		}

		public IProxyInterceptorDefinitionBuildStage<TObject> ReturningPrototypes()
		{
			_builder.BindProperty(f => f.IsSingleton).ToValue(false);
			return this;
		}

		public IProxyInterceptorDefinitionBuildStage<TObject> ReturningSingleton()
		{
			_builder.BindProperty(f => f.IsSingleton).ToValue(true);
			return this;
		}
	}
}