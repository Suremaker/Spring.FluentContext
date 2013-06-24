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
using System.Linq.Expressions;
using Spring.FluentContext.Builders;

namespace Spring.FluentContext.BuildingStages.Objects
{
	/// <summary>
	/// Interface for instantiation build stage.
	/// </summary>
	/// <typeparam name="TObject">Type of configured object.</typeparam>
	public interface IInstantiationBuildStage<TObject> : ILooseCtorDefinitionBuildStage<TObject>
	{
		/// <summary>
		/// Specifies that object should be instantiated by calling static method specified by <c>factoryMethodSelector</c>.
		/// </summary>
		/// <param name="factoryMethodSelector">Lambda expression to select method to call.</param>
		/// <returns>Next build stage (omits IMethodConfigurationBuildStage).</returns>
		IAutoConfigurationBuildStage<TObject> UseStaticFactoryMethod(Func<TObject> factoryMethodSelector);

		/// <summary>
		/// Specifies that object should be instantiated by calling static method specified by <c>factoryMethodSelector</c>.
		/// </summary>
		/// <typeparam name="TArg1">Factory method argument.</typeparam>
		/// <param name="factoryMethodSelector">Lambda expression to select method to call.</param>
		/// <returns>Next build stage (omits IMethodConfigurationBuildStage).</returns>
		IFactoryMethodArgBuildStage<TObject, TArg1> UseStaticFactoryMethod<TArg1>(Expression<Func<TArg1, TObject>> factoryMethodSelector);

		/// <summary>
		/// Specifies that object should be instantiated by calling static method specified by <c>factoryMethodSelector</c>.
		/// </summary>
		/// <typeparam name="TArg1">Factory method argument 1.</typeparam>
		/// <typeparam name="TArg2">Factory method argument 2.</typeparam>
		/// <param name="factoryMethodSelector">Lambda expression to select method to call.</param>
		/// <returns>Next build stage (omits IMethodConfigurationBuildStage).</returns>
		IFactoryMethodArgBuildStage<TObject, TArg1, TArg2> UseStaticFactoryMethod<TArg1, TArg2>(Expression<Func<TArg1, TArg2, TObject>> factoryMethodSelector);

		/// <summary>
		/// Specifies that object should be instantiated by calling static method specified by <c>factoryMethodSelector</c>.
		/// </summary>
		/// <typeparam name="TArg1">Factory method argument 1.</typeparam>
		/// <typeparam name="TArg2">Factory method argument 2.</typeparam>
		/// <typeparam name="TArg3">Factory method argument 3.</typeparam>
		/// <param name="factoryMethodSelector">Lambda expression to select method to call.</param>
		/// <returns>Next build stage (omits IMethodConfigurationBuildStage).</returns>
		IFactoryMethodArgBuildStage<TObject, TArg1, TArg2, TArg3> UseStaticFactoryMethod<TArg1, TArg2, TArg3>(Expression<Func<TArg1, TArg2, TArg3, TObject>> factoryMethodSelector);

		/// <summary>
		/// Specifies that object should be instantiated by calling method specified by <c>factoryMethodSelector</c>.
		/// </summary>
		/// <typeparam name="TFactoryObject">Type of factory object.</typeparam>
		/// <param name="factoryMethodSelector">Lambda expression to select method to call.</param>
		/// <returns>Next build stage (omits IMethodConfigurationBuildStage).</returns>
		IFactoryMethodDefinitionBuilder<TFactoryObject, TObject> UseFactoryMethod<TFactoryObject>(Expression<Func<TFactoryObject, TObject>> factoryMethodSelector);

		/// <summary>
		/// Specifies that object should be instantiated by calling method specified by <c>factoryMethodSelector</c>.
		/// </summary>
		/// <typeparam name="TFactoryObject">Type of factory object.</typeparam>
		/// <typeparam name="TArg1">Factory method argument.</typeparam>
		/// <param name="factoryMethodSelector">Lambda expression to select method to call.</param>
		/// <returns>Next build stage (omits IMethodConfigurationBuildStage).</returns>
		IFactoryMethodDefinitionBuilder<TFactoryObject, TObject, TArg1> UseFactoryMethod<TFactoryObject, TArg1>(Expression<Func<TFactoryObject, TArg1, TObject>> factoryMethodSelector);

		/// <summary>
		/// Specifies that object should be instantiated by calling method specified by <c>factoryMethodSelector</c>.
		/// </summary>
		/// <typeparam name="TFactoryObject">Type of factory object.</typeparam>
		/// <typeparam name="TArg1">Factory method argument 1.</typeparam>
		/// <typeparam name="TArg2">Factory method argument 2.</typeparam>
		/// <param name="factoryMethodSelector">Lambda expression to select method to call.</param>
		/// <returns>Next build stage (omits IMethodConfigurationBuildStage).</returns>
		IFactoryMethodDefinitionBuilder<TFactoryObject, TObject, TArg1, TArg2> UseFactoryMethod<TFactoryObject, TArg1, TArg2>(Expression<Func<TFactoryObject, TArg1, TArg2, TObject>> factoryMethodSelector);

		/// <summary>
		/// Specifies that object should be instantiated by calling method specified by <c>factoryMethodSelector</c>.
		/// </summary>
		/// <typeparam name="TFactoryObject">Type of factory object.</typeparam>
		/// <typeparam name="TArg1">Factory method argument 1.</typeparam>
		/// <typeparam name="TArg2">Factory method argument 2.</typeparam>
		/// <typeparam name="TArg3">Factory method argument 3.</typeparam>
		/// <param name="factoryMethodSelector">Lambda expression to select method to call.</param>
		/// <returns>Next build stage (omits IMethodConfigurationBuildStage).</returns>
		IFactoryMethodDefinitionBuilder<TFactoryObject, TObject, TArg1, TArg2, TArg3> UseFactoryMethod<TFactoryObject, TArg1, TArg2, TArg3>(Expression<Func<TFactoryObject, TArg1, TArg2, TArg3, TObject>> factoryMethodSelector);

		/// <summary>
		/// Specifies that object should be instantiated by calling constructor specified by <c>constructorSelector</c>.
		/// </summary>
		/// <typeparam name="TArg">First constructor parameter type.</typeparam>
		/// <param name="constructorSelector">Lambda expression to select constructor to call.</param>
		/// <returns>Next build stage.</returns>
		ICtorDefinitionBuildStage<TObject, TArg> UseConstructor<TArg>(Func<TArg, TObject> constructorSelector);

		/// <summary>
		/// Specifies that object should be instantiated by calling constructor specified by <c>constructorSelector</c>.
		/// </summary>
		/// <typeparam name="TArg1">First constructor parameter type.</typeparam>
		/// <typeparam name="TArg2">Second constructor parameter type.</typeparam>
		/// <param name="constructorSelector">Lambda expression to select constructor to call.</param>
		/// <returns>Next build stage.</returns>
		ICtorDefinitionBuildStage<TObject, TArg1, TArg2> UseConstructor<TArg1, TArg2>(Func<TArg1, TArg2, TObject> constructorSelector);

		/// <summary>
		/// Specifies that object should be instantiated by calling constructor specified by <c>constructorSelector</c>.
		/// </summary>
		/// <typeparam name="TArg1">First constructor parameter type.</typeparam>
		/// <typeparam name="TArg2">Second constructor parameter type.</typeparam>
		/// <typeparam name="TArg3">Third constructor parameter type.</typeparam>
		/// <param name="constructorSelector">Lambda expression to select constructor to call.</param>
		/// <returns>Next build stage.</returns>
		ICtorDefinitionBuildStage<TObject, TArg1, TArg2, TArg3> UseConstructor<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, TObject> constructorSelector);

		/// <summary>
		/// Specifies that object should be instantiated by calling constructor specified by <c>constructorSelector</c>.
		/// </summary>
		/// <typeparam name="TArg1">First constructor parameter type.</typeparam>
		/// <typeparam name="TArg2">Second constructor parameter type.</typeparam>
		/// <typeparam name="TArg3">Third constructor parameter type.</typeparam>
		/// <typeparam name="TArg4">Fourth constructor parameter type.</typeparam>
		/// <param name="constructorSelector">Lambda expression to select constructor to call.</param>
		/// <returns>Next build stage.</returns>
		ICtorDefinitionBuildStage<TObject, TArg1, TArg2, TArg3, TArg4> UseConstructor<TArg1, TArg2, TArg3, TArg4>(Func<TArg1, TArg2, TArg3, TArg4, TObject> constructorSelector);
	}
}
