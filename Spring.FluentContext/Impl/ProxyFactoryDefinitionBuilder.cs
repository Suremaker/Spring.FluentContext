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

		public IProxyInstantiationDefinitionBuildStage<TObject> Targeting<TReferencedType>(ObjectRef<TReferencedType> reference) where TReferencedType : TObject
		{
			return Targeting(reference.Id);
		}

		public IProxyInterceptorDefinitionBuildStage<TObject> InterceptedBy(string objectId)
		{
			_interceptorNames.Add(objectId);
			_builder.BindPropertyNamed<string[]>("InterceptorNames").ToValue(_interceptorNames.ToArray());
			return this;
		}

        public IProxyInterceptorDefinitionBuildStage<TObject> InterceptedBy<TInterceptorType>(ObjectRef<TInterceptorType> reference) where TInterceptorType : IAdvice
		{
			return InterceptedBy(reference.Id);
		}

        public IProxyInterceptorDefinitionBuildStage<TObject> InterceptedByDefault<TInterceptorType>() where TInterceptorType : IAdvice
		{
			return InterceptedBy(IdGenerator<TInterceptorType>.GetDefaultId());
		}

		public ObjectRef<TObject> GetReference()
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