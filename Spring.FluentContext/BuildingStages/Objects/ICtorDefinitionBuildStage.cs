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

using Spring.FluentContext.Builders;

namespace Spring.FluentContext.BuildingStages.Objects
{
	/// <summary>
	/// Interface for constructor definition build stage.
	/// </summary>
	/// <typeparam name="TObject">Type of configured object.</typeparam>
	/// <typeparam name="TArg">Type of first constructor argument.</typeparam>
	public interface ICtorDefinitionBuildStage<TObject, TArg>
	{
		/// <summary>
		/// Binds constructor argument.
		/// </summary>
		/// <returns>Constructor argument definition builder instance.</returns>
		IMethodArgumentDefinitionBuilder<IMethodConfigurationBuildStage<TObject>, TArg> BindConstructorArg();
	}

	/// <summary>
	/// Interface for constructor definition build stage.
	/// </summary>
	/// <typeparam name="TObject">Type of configured object.</typeparam>
	/// <typeparam name="TArg1">Type of first constructor argument.</typeparam>
	/// <typeparam name="TArg2">Type of second constructor argument.</typeparam>
	public interface ICtorDefinitionBuildStage<TObject, TArg1, TArg2>
	{
		/// <summary>
		/// Binds constructor arguments.
		/// </summary>
		/// <returns>Constructor argument definition builder instance.</returns>
		IMethodArgumentDefinitionBuilder<ICtorDefinitionBuildStage<TObject, TArg2>, TArg1> BindConstructorArg();
	}

	/// <summary>
	/// Interface for constructor definition build stage.
	/// </summary>
	/// <typeparam name="TObject">Type of configured object.</typeparam>
	/// <typeparam name="TArg1">Type of first constructor argument.</typeparam>
	/// <typeparam name="TArg2">Type of second constructor argument.</typeparam>
	/// <typeparam name="TArg3">Type of third constructor argument.</typeparam>
	public interface ICtorDefinitionBuildStage<TObject, TArg1, TArg2, TArg3>
	{
		/// <summary>
		/// Binds constructor arguments.
		/// </summary>
		/// <returns>Constructor argument definition builder instance.</returns>
		IMethodArgumentDefinitionBuilder<ICtorDefinitionBuildStage<TObject, TArg2, TArg3>, TArg1> BindConstructorArg();
	}

	/// <summary>
	/// Interface for constructor definition build stage.
	/// </summary>
	/// <typeparam name="TObject">Type of configured object.</typeparam>
	/// <typeparam name="TArg1">Type of first constructor argument.</typeparam>
	/// <typeparam name="TArg2">Type of second constructor argument.</typeparam>
	/// <typeparam name="TArg3">Type of third constructor argument.</typeparam>
	/// <typeparam name="TArg4">Type of fourth constructor argument.</typeparam>
	public interface ICtorDefinitionBuildStage<TObject, TArg1, TArg2, TArg3, TArg4>
	{
		/// <summary>
		/// Binds constructor arguments.
		/// </summary>
		/// <returns>Constructor argument definition builder instance.</returns>
		IMethodArgumentDefinitionBuilder<ICtorDefinitionBuildStage<TObject, TArg2, TArg3, TArg4>, TArg1> BindConstructorArg();
	}
}
