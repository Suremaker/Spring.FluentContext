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
using Spring.FluentContext.BuildingStages.Aliases;
using Spring.FluentContext.BuildingStages.Objects;
using Spring.FluentContext.BuildingStages.ProxyFactories;
using Spring.FluentContext.Impl;
using Spring.FluentContext.Utils;
using Spring.Objects.Factory.Support;

namespace Spring.FluentContext
{
	/// <summary>
	/// Class offering fluent API for object definition construction.
	/// </summary>
	public class FluentApplicationContext : GenericApplicationContext, IFluentApplicationContext
	{
		/// <summary>
		/// Creates new context.
		/// </summary>
		public FluentApplicationContext()
		{
		}

		/// <summary>
		/// Creates new context.
		/// </summary>
		/// <param name="caseSensitive">if set to <c>true</c> names in the context are case sensitive.</param>
		public FluentApplicationContext(bool caseSensitive)
			: base(caseSensitive)
		{
		}

		/// <summary>
		/// Creates new context.
		/// </summary>
		/// <param name="objectFactory">The object factory instance to use for this context.</param>
		public FluentApplicationContext(DefaultListableObjectFactory objectFactory)
			: base(objectFactory)
		{
		}

		/// <summary>
		/// Creates new context.
		/// </summary>
		/// <param name="parent">The parent application context.</param>
		public FluentApplicationContext(IApplicationContext parent)
			: base(parent)
		{
		}

		/// <summary>
		/// Creates new context.
		/// </summary>
		/// <param name="name">The name of the application context.</param>
		/// <param name="caseSensitive">if set to <c>true</c> names in the context are case sensitive.</param>
		/// <param name="parent">The parent application context.</param>
		public FluentApplicationContext(string name, bool caseSensitive, IApplicationContext parent)
			: base(name, caseSensitive, parent)
		{
		}

		/// <summary>
		/// Creates new context.
		/// </summary>
		/// <param name="objectFactory">The object factory to use for this context</param>
		/// <param name="parent">The parent application context.</param>
		public FluentApplicationContext(DefaultListableObjectFactory objectFactory, IApplicationContext parent)
			: base(objectFactory, parent)
		{
		}

		/// <summary>
		/// Creates new context.
		/// </summary>
		/// <param name="name">The name of the application context.</param>
		/// <param name="caseSensitive">if set to <c>true</c> names in the context are case sensitive.</param>
		/// <param name="parent">The parent application context.</param>
		/// <param name="objectFactory">The object factory to use for this context</param>
		public FluentApplicationContext(string name, bool caseSensitive, IApplicationContext parent, DefaultListableObjectFactory objectFactory)
			: base(name, caseSensitive, parent, objectFactory)
		{
		}

		/// <summary>
		/// Registers alias with default id for object of <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type of object returned by alias.</typeparam>
		/// <returns>Next build stage.</returns>
		public IAliasLinkingBuildStage<T> RegisterDefaultAlias<T>()
		{
			return RegisterNamedAlias<T>(IdGenerator<T>.GetDefaultId());
		}

		/// <summary>
		/// Registers alias with unique id for object of <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type of object returned by alias.</typeparam>
		/// <returns>Next build stage.</returns>
		public IAliasLinkingBuildStage<T> RegisterUniquelyNamedAlias<T>()
		{
			return RegisterNamedAlias<T>(IdGenerator<T>.GetUniqueId());
		}

		/// <summary>
		/// Registers alias with <c>id</c> for object of <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type of object returned by alias.</typeparam>
		/// <param name="id">Alias id.</param>
		/// <returns>Next build stage.</returns>
		public IAliasLinkingBuildStage<T> RegisterNamedAlias<T>(string id)
		{
			return new AliasDefinitionBuilder<T>(this, id);
		}

		/// <summary>
		/// Registers object definition with specified <c>id</c> for <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type of configured object.</typeparam>
		/// <param name="id">Object id.</param>
		/// <returns>Next build stage.</returns>
		public IScopeBuildStage<T> RegisterNamed<T>(string id)
		{
			var builder = new ObjectDefinitionBuilder<T>(id);
			RegisterObjectDefinition(id, builder.Definition);
			return builder;
		}

		/// <summary>
		/// Registers object definition with default id for <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type of configured object.</typeparam>
		/// <returns>Next build stage.</returns>
		public IScopeBuildStage<T> RegisterDefault<T>()
		{
			return RegisterNamed<T>(IdGenerator<T>.GetDefaultId());
		}

		/// <summary>
		/// Registers object definition with unique id for <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type of configured object.</typeparam>
		/// <returns>Next build stage.</returns>
		public IScopeBuildStage<T> RegisterUniquelyNamed<T>()
		{
			return RegisterNamed<T>(IdGenerator<T>.GetUniqueId());
		}

		/// <summary>
		/// Registers proxy factory with specified <c>id</c> for proxies of <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type to proxy.</typeparam>
		/// <param name="id">Proxy definition id.</param>
		/// <returns>Next build stage.</returns>
		public IProxyTargetDefinitionBuildStage<T> RegisterNamedProxyFactory<T>(string id)
		{
			var builder = new ProxyFactoryDefinitionBuilder<T>(id);
			RegisterObjectDefinition(id, builder.Definition);
			return builder;
		}

		/// <summary>
		/// Registers proxy factory with default id for proxies of <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type to proxy.</typeparam>
		/// <returns>Next build stage.</returns>
		public IProxyTargetDefinitionBuildStage<T> RegisterDefaultProxyFactory<T>()
		{
			return RegisterNamedProxyFactory<T>(IdGenerator<T>.GetDefaultId());
		}

		/// <summary>
		/// Registers proxy factory with unique id for proxies of <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type to proxy.</typeparam>
		/// <returns>Next build stage.</returns>
		public IProxyTargetDefinitionBuildStage<T> RegisterUniquelyNamedProxyFactory<T>()
		{
			return RegisterNamedProxyFactory<T>(IdGenerator<T>.GetUniqueId());
		}

		/// <summary>
		/// Registers singleton <c>instance</c> with specified <c>id</c>.
		/// </summary>
		/// <typeparam name="T">Type of singleton instance.</typeparam>
		/// <param name="id">Id of registered object.</param>
		/// <param name="instance">Singleton instance to register.</param>
		/// <returns>Registered object reference.</returns>
		public IObjectRef<T> RegisterNamedSingleton<T>(string id, T instance)
		{
			ObjectFactory.RegisterSingleton(id, instance);
			return new ObjectRef<T>(id);
		}

		/// <summary>
		/// Registers singleton <c>instance</c> with default id.
		/// </summary>
		/// <typeparam name="T">Type of singleton instance.</typeparam>		
		/// <param name="instance">Singleton instance to register.</param>
		/// <returns>Registered object reference.</returns>
		public IObjectRef<T> RegisterDefaultSingleton<T>(T instance)
		{
			return RegisterNamedSingleton(IdGenerator<T>.GetDefaultId(), instance);
		}

		/// <summary>
		/// Registers singleton <c>instance</c> with unique id.
		/// </summary>
		/// <typeparam name="T">Type of singleton instance.</typeparam>		
		/// <param name="instance">Singleton instance to register.</param>
		/// <returns>Registered object reference.</returns>
		public IObjectRef<T> RegisterUniquelyNamedSingleton<T>(T instance)
		{
			return RegisterNamedSingleton(IdGenerator<T>.GetUniqueId(), instance);
		}
	}
}