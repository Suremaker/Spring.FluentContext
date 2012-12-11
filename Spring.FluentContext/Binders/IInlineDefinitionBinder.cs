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

using System;
using Spring.FluentContext.BuildingStages.Objects;

namespace Spring.FluentContext.Binders
{
	/// <summary>
	/// Interface for inline definition binder.
	/// </summary>
	/// <typeparam name="TBuilder">Type of builder returned after binding is done.</typeparam>
	/// <typeparam name="TTargetType">Type of binding.</typeparam>
	public interface IInlineDefinitionBinder<out TBuilder, in TTargetType>
	{

		/// <summary>
		/// Specifies an inline object definition as binding target, where targeted object is defined by <c>innerObjectBuildAction</c> action.
		/// </summary>
		/// <typeparam name="TInnerObject">Type of object that would be instantiated.</typeparam>
		/// <param name="innerObjectBuildAction">Inline definition build action.</param>
		/// <returns>Builder.</returns>
		TBuilder ToInlineDefinition<TInnerObject>(Action<IInstantiationBuildStage<TInnerObject>> innerObjectBuildAction) where TInnerObject : TTargetType;

		/// <summary>
		/// Specifies an inline, default object definition as binding target.
		/// This method is useful if specific implementation has to be injected, where it does not require additional configuration.
		/// </summary>
		/// <typeparam name="TInnerObject">Type of object that would be instantiated.</typeparam>
		/// <returns>Builder.</returns>
		TBuilder ToInlineDefinition<TInnerObject>() where TInnerObject : TTargetType;
	}
}