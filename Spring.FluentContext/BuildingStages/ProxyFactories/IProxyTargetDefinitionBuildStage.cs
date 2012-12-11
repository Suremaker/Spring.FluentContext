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

namespace Spring.FluentContext.BuildingStages.ProxyFactories
{
	/// <summary>
	/// Interface for proxy target definition stage.
	/// </summary>
	/// <typeparam name="TObject">Type of objects created by factory.</typeparam>
	public interface IProxyTargetDefinitionBuildStage<TObject>
	{
		/// <summary>
		/// Specifies object with <c>objectId</c> id to be proxy target.
		/// </summary>
		/// <param name="objectId">Object definition id.</param>
		/// <returns>Next build stage.</returns>
		IProxyInstantiationDefinitionBuildStage<TObject> Targeting(string objectId);

		/// <summary>
		/// Specifies object with default id for <c>TReferencedType</c> type to be proxy target.
		/// </summary>
		/// <typeparam name="TReferencedType">Type of targeted object.</typeparam>
		/// <returns>Next build stage.</returns>
		IProxyInstantiationDefinitionBuildStage<TObject> TargetingDefault<TReferencedType>() where TReferencedType : TObject;

		/// <summary>
		/// Specifies object referenced with <c>reference</c> to be proxy target.
		/// </summary>
		/// <param name="reference">Object definition reference.</param>
		/// <returns>Next build stage.</returns>
		IProxyInstantiationDefinitionBuildStage<TObject> Targeting(IObjectRef<TObject> reference);
	}
}