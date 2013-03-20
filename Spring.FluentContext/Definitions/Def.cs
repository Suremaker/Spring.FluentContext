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

using System;
using Spring.FluentContext.BuildingStages.Objects;
using Spring.FluentContext.Impl;

namespace Spring.FluentContext.Definitions
{
	/// <summary>
	/// Class allowing to create inline object definitions.
	/// </summary>
	public static class Def
	{
		/// <summary>
		/// Creates inline object definition using <c>objectBuildAction</c>.
		/// </summary>
		/// <typeparam name="TObject">Type of defined object.</typeparam>
		/// <param name="objectBuildAction">Action used to configure object.</param>
		/// <returns>Definition.</returns>
		public static IDefinition<TObject> Inline<TObject>(Action<IInstantiationBuildStage<TObject>> objectBuildAction)
		{
			var builder = new ObjectDefinitionBuilder<TObject>(null);
			objectBuildAction(builder);
			return new Definition<TObject>(builder.Definition);
		}
	}
}
