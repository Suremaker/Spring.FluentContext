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

using Spring.FluentContext.Builders;

namespace Spring.FluentContext.BuildingStages.Objects
{
	/// <summary>
	/// Interface for factory method argument build stage.
	/// </summary>
	/// <typeparam name="TObject">Type of configured object.</typeparam>
	/// <typeparam name="TArg1">Type of first factory method argument.</typeparam>
	public interface IFactoryMethodArgBuildStage<TObject, TArg1>
	{
		/// <summary>
		/// Binds factory method argument.
		/// </summary>
		/// <returns>Argument definition builder.</returns>
		IMethodArgumentDefinitionBuilder<IAutoConfigurationBuildStage<TObject>, TArg1> BindMethodArg();
	}

	/// <summary>
	/// Interface for factory method argument build stage.
	/// </summary>
	/// <typeparam name="TObject">Type of configured object.</typeparam>
	/// <typeparam name="TArg1">Type of first factory method argument.</typeparam>
	/// <typeparam name="TArg2">Type of second factory method argument.</typeparam>
	public interface IFactoryMethodArgBuildStage<TObject, TArg1, TArg2>
	{
		/// <summary>
		/// Binds factory method argument.
		/// </summary>
		/// <returns>Argument definition builder.</returns>
		IMethodArgumentDefinitionBuilder<IFactoryMethodArgBuildStage<TObject, TArg2>, TArg1> BindMethodArg();
	}

	/// <summary>
	/// Interface for factory method argument build stage.
	/// </summary>
	/// <typeparam name="TObject">Type of configured object.</typeparam>
	/// <typeparam name="TArg1">Type of first factory method argument.</typeparam>
	/// <typeparam name="TArg2">Type of second factory method argument.</typeparam>
	/// <typeparam name="TArg3">Type of third factory method argument.</typeparam>
	public interface IFactoryMethodArgBuildStage<TObject, TArg1, TArg2, TArg3>
	{
		/// <summary>
		/// Binds factory method argument.
		/// </summary>
		/// <returns>Argument definition builder.</returns>
		IMethodArgumentDefinitionBuilder<IFactoryMethodArgBuildStage<TObject, TArg2, TArg3>, TArg1> BindMethodArg();
	}
}