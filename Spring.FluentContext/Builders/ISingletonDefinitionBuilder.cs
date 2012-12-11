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

namespace Spring.FluentContext.Builders
{
	/// <summary>
	/// Interface for singleton instances registration.
	/// </summary>
	public interface ISingletonDefinitionBuilder
	{
		/// <summary>
		/// Registers singleton <c>instance</c> with specified <c>id</c>.
		/// </summary>
		/// <typeparam name="T">Type of singleton instance.</typeparam>
		/// <param name="id">Id of registered object.</param>
		/// <param name="instance">Singleton instance to register.</param>
		/// <returns>Registered object reference.</returns>
		IObjectRef<T> RegisterNamedSingleton<T>(string id, T instance);

		/// <summary>
		/// Registers singleton <c>instance</c> with default id.
		/// </summary>
		/// <typeparam name="T">Type of singleton instance.</typeparam>		
		/// <param name="instance">Singleton instance to register.</param>
		/// <returns>Registered object reference.</returns>
		IObjectRef<T> RegisterDefaultSingleton<T>(T instance);

		/// <summary>
		/// Registers singleton <c>instance</c> with unique id.
		/// </summary>
		/// <typeparam name="T">Type of singleton instance.</typeparam>		
		/// <param name="instance">Singleton instance to register.</param>
		/// <returns>Registered object reference.</returns>
		IObjectRef<T> RegisterUniquelyNamedSingleton<T>(T instance);
	}
}