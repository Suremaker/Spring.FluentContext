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
	/// Interface for object configuration build stage.
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public interface IObjectConfigurationBuildStage<TObject> : IInitBehaviorBuildStage<TObject>
	{
		/// <summary>
		/// Binds property specified by <c>propertySelector</c>.
		/// </summary>
		/// <typeparam name="TProperty">Type of property.</typeparam>
		/// <param name="propertySelector">Lambda expression to select property.</param>
		/// <returns>IPropertyDefinitionBuilder instance.</returns>
		IPropertyDefinitionBuilder<TObject, TProperty> BindProperty<TProperty>(Expression<Func<TObject, TProperty>> propertySelector);

		/// <summary>
		/// Binds property specified by <c>propertyName</c>.
		/// </summary>
		/// <typeparam name="TProperty">Type of property.</typeparam>
		/// <param name="propertyName">Property name.</param>
		/// <returns>IPropertyDefinitionBuilder instance.</returns>
		IPropertyDefinitionBuilder<TObject, TProperty> BindPropertyNamed<TProperty>(string propertyName);		
	}
}
