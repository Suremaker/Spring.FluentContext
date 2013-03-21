//
//  Author:
//    Wojciech Kotlarski
//
//  Copyright (c) 2013, Wojciech Kotlarski
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
using System.Collections.Generic;
using System.Linq;
using Spring.FluentContext.Builders;
using Spring.FluentContext.BuildingStages.Objects;
using Spring.FluentContext.Impl;
using Spring.FluentContext.Utils;
using Spring.Objects.Factory.Config;

namespace Spring.FluentContext.Definitions
{
	/// <summary>
	/// Class allowing to create various object definitions.
	/// </summary>
	public class Def
	{
		/// <summary>
		/// Creates object definition using <c>objectBuildAction</c>.
		/// </summary>
		/// <typeparam name="TObject">Type of defined object.</typeparam>
		/// <param name="objectBuildAction">Action used to configure object.</param>
		/// <returns>Definition.</returns>
		public static IDefinition<TObject> Object<TObject>(Action<IInstantiationBuildStage<TObject>> objectBuildAction)
		{
			var builder = new ObjectDefinitionBuilder<TObject>(null);
			objectBuildAction(builder);
			return new Definition<TObject>(builder.Definition);
		}

		/// <summary>
		/// Creates definition referencing to object of <c>TTargetType</c> type with default id.
		/// </summary>
		/// <typeparam name="TTargetType">Type of referenced object.</typeparam>
		/// <returns>Definition.</returns>
		public static IObjectRef<TTargetType> Reference<TTargetType>()
		{
			return Reference<TTargetType>(IdGenerator<TTargetType>.GetDefaultId());
		}

		/// <summary>
		/// Creates definition referencing to object of <c>TTargetType</c> type with <c>objectId</c> id.
		/// </summary>
		/// <typeparam name="TTargetType">Type of referenced object.</typeparam>
		/// <param name="objectId">Id of referenced object</param>
		/// <returns>Definition.</returns>
		public static IObjectRef<TTargetType> Reference<TTargetType>(string objectId)
		{
			return new ObjectRef<TTargetType>(objectId);
		}

		/// <summary>
		/// Creates definition of constant <c>value</c> of <c>TValue</c> type.
		/// </summary>
		/// <typeparam name="TValue">Type of constant value.</typeparam>
		/// <param name="value">Value.</param>
		/// <returns>Definition.</returns>
		public static IDefinition<TValue> Value<TValue>(TValue value)
		{
			return new Definition<TValue>(value);
		}

		/// <summary>
		/// Creates definition of array of <c>TElement</c> type, containing elements described by <c>items</c> definitions.
		/// </summary>
		/// <typeparam name="TElement">Array item type.</typeparam>
		/// <param name="items">Definitions of array items.</param>
		/// <returns>Definition.</returns>
		public static IDefinition<TElement[]> Array<TElement>(params IDefinition<TElement>[] items)
		{
			return new Definition<TElement[]>(ToList(items));
		}

		/// <summary>
		/// Creates definition of list of <c>TElement</c> type, containing elements described by <c>items</c> definitions.
		/// </summary>
		/// <typeparam name="TElement">List item type.</typeparam>
		/// <param name="items">Definitions of list items.</param>
		/// <returns>Definition.</returns>
		public static IDefinition<List<TElement>> List<TElement>(params IDefinition<TElement>[] items)
		{
			return new Definition<List<TElement>>(ToList(items));
		}

		/// <summary>
		/// Creates definition of dictionary, where items are filled by <c>dictionaryBuilder</c> action.
		/// </summary>
		/// <typeparam name="TKey">Dictionary key type.</typeparam>
		/// <typeparam name="TValue">Dictionary value type.</typeparam>
		/// <param name="dictionaryBuilder">Dictionary builder action filling constructed dictionary with key-value definitions.</param>
		/// <returns>Definition.</returns>
		public static IDefinition<Dictionary<TKey, TValue>> Dictionary<TKey, TValue>(Action<IDictionaryDefinitionBuilder<TKey, TValue>> dictionaryBuilder)
		{
			var builder = new DictionaryDefinitionBuilder<TKey, TValue>();
			dictionaryBuilder(builder);
			return new Definition<Dictionary<TKey, TValue>>(builder.Dictionary);
		}

		private static IManagedCollection ToList<TTargetType>(IEnumerable<IDefinition<TTargetType>> items)
		{
			var list = new ManagedList { ElementTypeName = typeof(TTargetType).FullName };
			foreach (var def in items.Select(i => i.DefinitionObject))
				list.Add(def);
			return list;
		}
	}
}
