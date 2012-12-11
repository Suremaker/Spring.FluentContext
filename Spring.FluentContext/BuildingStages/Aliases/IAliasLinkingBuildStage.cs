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

namespace Spring.FluentContext.BuildingStages.Aliases
{
	/// <summary>
	/// Interface for alias linking build stage
	/// </summary>
	/// <typeparam name="TObject">Type of alias.</typeparam>
	public interface IAliasLinkingBuildStage<TObject>
	{
		/// <summary>
		/// Specifies object with default id for <c>TDerived</c> type to be alias target.
		/// </summary>
		/// <typeparam name="TDerived">Type of targeted object.</typeparam>
		/// <returns>Next build stage.</returns>
		IReferencingStage<TObject> ToRegisteredDefault<TDerived>() where TDerived : TObject;

		/// <summary>
		/// Specifies object with specified <c>objectId</c> id for <c>TDerived</c> type to be alias target.
		/// </summary>
		/// <typeparam name="TDerived">Type of targeted object.</typeparam>
		/// <param name="objectId">Object definition id.</param>
		/// <returns>Next build stage.</returns>
		IReferencingStage<TObject> ToRegistered<TDerived>(string objectId) where TDerived : TObject;

		/// <summary>
		/// Specifies object referenced with <c>reference</c> to be alias target.
		/// </summary>
		/// <param name="reference">Object definition reference.</param>
		/// <returns>Next build stage.</returns>
		IReferencingStage<TObject> ToRegistered(IObjectRef<TObject> reference);
	}

}