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
using Spring.FluentContext.Binders;
using Spring.FluentContext.Builders;
using Spring.FluentContext.Definitions;

namespace Spring.FluentContext
{
	/// <summary>
	/// Binder extension class helping with binding constructor / property values to definitions such as collections
	/// </summary>
	public static class BinderExtensions
	{
		/// <summary>
		/// Allows to bind given <c>values</c> to constructor argument or property value defined as a list.
		/// </summary>
		/// <typeparam name="TBuilder">Result builder.</typeparam>
		/// <typeparam name="TElement">Type of collection elements.</typeparam>
		/// <param name="binder">Binder.</param>
		/// <param name="values">Collection values.</param>
		/// <returns>Builder.</returns>
		public static TBuilder ToList<TBuilder, TElement>(this IDefinitionBinder<TBuilder,List<TElement>> binder, params IDefinition<TElement>[] values)
		{
			return binder.To(Def.List(values));
		}

		/// <summary>
		/// Allows to bind given <c>values</c> to constructor argument or property value defined as an array.
		/// </summary>
		/// <typeparam name="TBuilder">Result builder.</typeparam>
		/// <typeparam name="TElement">Type of collection elements.</typeparam>
		/// <param name="binder">Binder.</param>
		/// <param name="values">Collection values.</param>
		/// <returns>Builder.</returns>
		public static TBuilder ToArray<TBuilder, TElement>(this IDefinitionBinder<TBuilder, TElement[]> binder, params IDefinition<TElement>[] values)
		{
			return binder.To(Def.Array(values));
		}

		/// <summary>
		/// Allows to bind dictionary configured by given <c>dictionaryBuilder</c> action to constructor argument or property value.
		/// </summary>
		/// <typeparam name="TBuilder">Result builder.</typeparam>
		/// <typeparam name="TKey">Dictionary key type.</typeparam>
		/// <typeparam name="TValue">Dictionary value type.</typeparam>
		/// <param name="binder">Binder.</param>
		/// <param name="dictionaryBuilder">Dictionary builder action filling constructed dictionary with key-value definitions.</param>
		/// <returns>Builder.</returns>
		public static TBuilder ToDictionary<TBuilder, TKey, TValue>(this IDefinitionBinder<TBuilder, Dictionary<TKey,TValue>> binder, Action<IDictionaryDefinitionBuilder<TKey, TValue>> dictionaryBuilder)
		{
			return binder.To(Def.Dictionary(dictionaryBuilder));
		}
	}
}
