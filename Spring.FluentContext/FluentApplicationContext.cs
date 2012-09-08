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
using Spring.Context;
using Spring.Context.Support;
using Spring.FluentContext.BuildingStages;
using Spring.FluentContext.Impl;
using Spring.FluentContext.Utils;
using Spring.Objects.Factory.Support;
using Spring.FluentContext.Builders;

namespace Spring.FluentContext
{
	public class FluentApplicationContext : GenericApplicationContext
	{
		public FluentApplicationContext()
		{
		}

		public FluentApplicationContext(bool caseSensitive) : base(caseSensitive)
		{
		}

		public FluentApplicationContext(DefaultListableObjectFactory objectFactory) : base(objectFactory)
		{
		}

		public FluentApplicationContext(IApplicationContext parent) : base(parent)
		{
		}

		public FluentApplicationContext(string name, bool caseSensitive, IApplicationContext parent) : base(name, caseSensitive, parent)
		{
		}

		public FluentApplicationContext(DefaultListableObjectFactory objectFactory, IApplicationContext parent) : base(objectFactory, parent)
		{
		}

		public FluentApplicationContext(string name, bool caseSensitive, IApplicationContext parent, DefaultListableObjectFactory objectFactory) : base(name, caseSensitive, parent, objectFactory)
		{
		}

		public IAliasDefinitionBuilder<T> RegisterDefaultAlias<T>()
		{
			return RegisterNamedAlias<T>(IdGenerator<T>.GetDefaultId());
		}

		public IAliasDefinitionBuilder<T> RegisterUniquelyNamedAlias<T>()
		{
			return RegisterNamedAlias<T>(IdGenerator<T>.GetUniqueId());
		}

		public IAliasDefinitionBuilder<T> RegisterNamedAlias<T>(string id)
		{
			return new AliasDefinitionBuilder<T>(this, id);
		}

		public IScopeBuildStage<T> RegisterNamed<T>(string id)
		{
			var builder = new ObjectDefinitionBuilder<T>(id);
			RegisterObjectDefinition(id, builder.Definition);
			return builder;
		}

		public IScopeBuildStage<T> RegisterDefault<T>()
		{
			return RegisterNamed<T>(IdGenerator<T>.GetDefaultId());
		}

		public IScopeBuildStage<T> RegisterUniquelyNamed<T>()
		{
			return RegisterNamed<T>(IdGenerator<T>.GetUniqueId());
		}

		public IProxyFactoryDefinitionBuilder<T> RegisterNamedProxyFactory<T>(string id)
		{
			var builder = new ProxyFactoryDefinitionBuilder<T>(id);
			RegisterObjectDefinition(id, builder.Definition);
			return builder;
		}

		public IProxyFactoryDefinitionBuilder<T> RegisterDefaultProxyFactory<T>()
		{
			return RegisterNamedProxyFactory<T>(IdGenerator<T>.GetDefaultId());
		}

		public IProxyFactoryDefinitionBuilder<T> RegisterUniquelyNamedProxyFactory<T>()
		{
			return RegisterNamedProxyFactory<T>(IdGenerator<T>.GetUniqueId());
		}
	}
}