using System.Collections.Generic;
using Spring.Aop.Framework;
using Spring.FluentContext.Utils;
using Spring.Objects.Factory.Config;

namespace Spring.FluentContext.Impl
{
	public class ProxyFactoryDefinitionBuilder<TObject> : IProxyFactoryDefinitionBuilder<TObject>
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

		public IProxyFactoryDefinitionBuilder<TObject> Targeting(string objectId)
		{
			_builder.BindPropertyNamed<string>("TargetName").ToValue(objectId);
			return this;
		}

		public IProxyFactoryDefinitionBuilder<TObject> TargetingDefaultOfType<TReferencedType>() where TReferencedType : TObject
		{
			return Targeting(IdGenerator<TReferencedType>.GetDefaultId());
		}

		public IProxyFactoryDefinitionBuilder<TObject> Targeting<TReferencedType>(ObjectRef<TReferencedType> reference) where TReferencedType : TObject
		{
			return Targeting(reference.Id);
		}

		public IProxyFactoryDefinitionBuilder<TObject> AddInterceptor(string objectId)
		{
			_interceptorNames.Add(objectId);
			_builder.BindPropertyNamed<string[]>("InterceptorNames").ToValue(_interceptorNames.ToArray());
			return this;
		}

		public IProxyFactoryDefinitionBuilder<TObject> AddInterceptor<TInterceptorType>(ObjectRef<TInterceptorType> reference)
		{
			return AddInterceptor(reference.Id);
		}

		public IProxyFactoryDefinitionBuilder<TObject> AddInterceptorByDefaultReference<TInterceptorType>()
		{
			return AddInterceptor(IdGenerator<TInterceptorType>.GetDefaultId());
		}

		public ObjectRef<TObject> GetReference()
		{
			return _ref;
		}
	}
}