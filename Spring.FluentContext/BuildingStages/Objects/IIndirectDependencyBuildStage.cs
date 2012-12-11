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

namespace Spring.FluentContext.BuildingStages.Objects
{
	/// <summary>
	/// Interface for indirect dependency build stage.
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public interface IIndirectDependencyBuildStage<TObject> : IInstantiationBuildStage<TObject>
	{
		/// <summary>
		/// Specifies that object with default id for <c>TOtherObject</c> type has to be configured before one that is currently being defined.
		/// </summary>
		/// <typeparam name="TOtherObject">Type of object.</typeparam>
		/// <returns>Same build stage.</returns>
		IIndirectDependencyBuildStage<TObject> DependingOnDefault<TOtherObject>();

		/// <summary>
		/// Specifies that object with specified <c>id</c> for <c>TOtherObject</c> type has to be configured before one that is currently being defined.
		/// </summary>
		/// <typeparam name="TOtherObject">Type of object.</typeparam>
		/// <param name="objectId">Object id.</param>
		/// <returns>Same build stage.</returns>
		IIndirectDependencyBuildStage<TObject> DependingOn<TOtherObject>(string objectId);

		/// <summary>
		/// Specifies that object referenced with <c>reference</c> has to be configured before one that is currently being defined.
		/// </summary>
		/// <typeparam name="TOtherObject">Type of object.</typeparam>
		/// <param name="reference">Object definition reference.</param>
		/// <returns>Same build stage.</returns>
		IIndirectDependencyBuildStage<TObject> DependingOn<TOtherObject>(IObjectRef<TOtherObject> reference);
	}
}
