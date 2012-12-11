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
using Spring.FluentContext.Utils;

namespace Spring.FluentContext
{
	/// <summary>
	/// IApplication context extensions class.
	/// </summary>
	public static class ContextExtensions
	{
		/// <summary>
		/// Returns object of <c>T</c> type, registered with specified <c>name</c>/id.
		/// </summary>
		/// <typeparam name="T">Type of object.</typeparam>
		/// <param name="ctx">Context.</param>
		/// <param name="name">Object name/id.</param>
		/// <returns>Registered object.</returns>
		public static T GetObject<T>(this IApplicationContext ctx, string name)
		{
			return (T)ctx.GetObject(name, typeof(T));
		}

		/// <summary>
		/// Returns object of <c>T</c> type, registered with default name/id.
		/// </summary>
		/// <typeparam name="T">Type of object.</typeparam>
		/// <param name="ctx">Context.</param>
		/// <returns>Registered object.</returns>
		public static T GetObject<T>(this IApplicationContext ctx)
		{
			return ctx.GetObject<T>(IdGenerator<T>.GetDefaultId());
		}

		/// <summary>
		/// Returns object of <c>T</c> type, referenced by <c>reference</c>.
		/// </summary>
		/// <typeparam name="T">Type of object.</typeparam>
		/// <param name="ctx">Context.</param>
		/// <param name="reference">Object definition reference.</param>
		/// <returns>Registered object.</returns>
		public static T GetObject<T>(this IApplicationContext ctx, IObjectRef<T> reference)
		{
			return ctx.GetObject<T>(reference.Id);
		}
	}
}
