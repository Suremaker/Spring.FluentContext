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

using AopAlliance.Aop;

namespace Spring.FluentContext.BuildingStages.ProxyFactories
{
	/// <summary>
	/// Interface for proxy interceptor definition stage.
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public interface IProxyInterceptorDefinitionBuildStage<TObject>: IReferencingStage<TObject>
	{
		/// <summary>
		/// Specifies that each proxy call would be intercepted by interceptor with <c>objectId</c> id.
		/// </summary>
		/// <param name="objectId">Interceptor object definition id.</param>
		/// <returns>Same build stage.</returns>
		IProxyInterceptorDefinitionBuildStage<TObject> InterceptedBy(string objectId);

		/// <summary>
		/// Specifies that each proxy call would be intercepted by interceptor referenced with <c>reference</c>.
		/// </summary>
		/// <typeparam name="TInterceptorType">Type of interceptor.</typeparam>
		/// <param name="reference">Interceptor object reference.</param>
		/// <returns>Same build stage.</returns>
        IProxyInterceptorDefinitionBuildStage<TObject> InterceptedBy<TInterceptorType>(IObjectRef<TInterceptorType> reference) where TInterceptorType : IAdvice;

		/// <summary>
		/// Specifies that each proxy call would be intercepted by interceptor with default id for <c>TInterceptorType</c> type.
		/// </summary>
		/// <typeparam name="TInterceptorType">Type of interceptor.</typeparam>
		/// <returns>Same stage.</returns>
        IProxyInterceptorDefinitionBuildStage<TObject> InterceptedByDefault<TInterceptorType>() where TInterceptorType : IAdvice;
	}
}