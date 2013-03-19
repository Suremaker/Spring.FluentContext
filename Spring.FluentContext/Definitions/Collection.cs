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

using System.Collections.Generic;
using System.Linq;
using Spring.Objects.Factory.Config;

namespace Spring.FluentContext.Definitions
{
	/// <summary>
	/// Class allowing to create definitions of collections, where items can be described with another definitions.
	/// </summary>
	public static class Collection
	{
		/// <summary>
		/// Creates definition of array of <c>TTargetType</c> type, containing elements described by <c>items</c> definitions.
		/// </summary>
		/// <typeparam name="TTargetType">Array item type.</typeparam>
		/// <param name="items">Definitions of array items.</param>
		/// <returns>Definition.</returns>
		public static IDefinition<TTargetType[]> Array<TTargetType>(params IDefinition<TTargetType>[] items)
		{
			return new Definition<TTargetType[]>(ToList(items));
		}

		/// <summary>
		/// Creates definition of list of <c>TTargetType</c> type, containing elements described by <c>items</c> definitions.
		/// </summary>
		/// <typeparam name="TTargetType">List item type.</typeparam>
		/// <param name="items">Definitions of list items.</param>
		/// <returns>Definition.</returns>
		public static IDefinition<List<TTargetType>> List<TTargetType>(params IDefinition<TTargetType>[] items)
		{
			return new Definition<List<TTargetType>>(ToList(items));
		}

		private static ManagedList ToList<TTargetType>(IEnumerable<IDefinition<TTargetType>> items)
		{
			var list = new ManagedList {ElementTypeName = typeof (TTargetType).FullName};
			foreach (var def in items.Select(i => i.DefinitionObject))
				list.Add(def);
			return list;
		}

	}
}
