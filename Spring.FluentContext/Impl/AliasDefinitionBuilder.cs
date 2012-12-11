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

using Spring.Context.Support;
using Spring.FluentContext.BuildingStages;
using Spring.FluentContext.BuildingStages.Aliases;
using Spring.FluentContext.Utils;

namespace Spring.FluentContext.Impl
{
	internal class AliasDefinitionBuilder<TObject> : IAliasLinkingBuildStage<TObject>, IReferencingStage<TObject>
	{
		private readonly string _alias;
		private readonly GenericApplicationContext _ctx;

		public AliasDefinitionBuilder(GenericApplicationContext ctx, string alias)
		{
			_ctx = ctx;
			_alias = alias;
		}

		public IReferencingStage<TObject> ToRegisteredDefault<TDerived>() where TDerived : TObject
		{
			return MakeAlias(IdGenerator<TDerived>.GetDefaultId());
		}

		public IReferencingStage<TObject> ToRegistered<TDerived>(string objectId) where TDerived : TObject
		{
			return MakeAlias(objectId);
		}

		public IReferencingStage<TObject> ToRegistered<TDerived>(ObjectRef<TDerived> reference) where TDerived : TObject
		{
			return MakeAlias(reference.Id);
		}

		public ObjectRef<TObject> GetReference()
		{
			return new ObjectRef<TObject>(_alias);
		}

		private IReferencingStage<TObject> MakeAlias(string objectId)
		{
			_ctx.RegisterAlias(objectId, _alias);
			return this;
		}
	}
}