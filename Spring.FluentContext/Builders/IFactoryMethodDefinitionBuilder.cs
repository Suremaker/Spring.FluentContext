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

using Spring.FluentContext.BuildingStages.Objects;

namespace Spring.FluentContext.Builders
{
	/// <summary>
	/// Interface for factory method definition builder.
	/// </summary>
	/// <typeparam name="TFactoryObject">Type of factory object.</typeparam>
	/// <typeparam name="TObject">Type of configured object.</typeparam>
	public interface IFactoryMethodDefinitionBuilder<TFactoryObject, TObject>
	{
		/// <summary>
		/// Specifies object with default id for <c>TObject</c> type to be factory object on which factory method would be executed.
		/// </summary>
		/// <returns>Next build stage.</returns>
		IAutoConfigurationBuildStage<TObject> OfRegisteredDefault();

		/// <summary>
		/// Specifies object with <c>objectId</c> id to be factory object on which factory method would be executed.
		/// </summary>
		/// <param name="objectId">Object definition id.</param>
		/// <returns>Next build stage.</returns>
		IAutoConfigurationBuildStage<TObject> OfRegistered(string objectId);

		/// <summary>
		/// Specifies object referenced with <c>reference</c> id to be factory object on which factory method would be executed.
		/// </summary>
		/// <param name="reference">Object definition reference.</param>
		/// <returns>Next build stage.</returns>
		IAutoConfigurationBuildStage<TObject> OfRegistered(IObjectRef<TFactoryObject> reference);
	}
}
