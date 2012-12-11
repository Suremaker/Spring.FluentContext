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
using Spring.FluentContext.Builders;
using Spring.FluentContext.BuildingStages;
using Spring.FluentContext.Utils;
using Spring.Objects.Factory.Config;

namespace Spring.FluentContext.Impl
{
	internal class CtorArgumentDefinitionBuilder<TBuilder, TArgument> : ICtorArgumentDefinitionBuilder<TBuilder, TArgument>
	{
		private readonly IDefinitionHolder _holder;
		private readonly TBuilder _builder;
		private readonly Action<ConstructorArgumentValues, object> _insertCtorArgAction;

		public CtorArgumentDefinitionBuilder(IDefinitionHolder holder, TBuilder builder, int argIndex)
		{
			_holder = holder;
			_builder = builder;
			_insertCtorArgAction = (list, value) => list.AddIndexedArgumentValue(argIndex, value, typeof(TArgument).FullName);
		}

		public CtorArgumentDefinitionBuilder(IDefinitionHolder holder, TBuilder builder)
		{
			_holder = holder;
			_builder = builder;
			_insertCtorArgAction = (list, value) => list.AddGenericArgumentValue(value, typeof(TArgument).FullName);
		}

		public TBuilder ToValue(TArgument value)
		{
			_insertCtorArgAction(_holder.Definition.ConstructorArgumentValues, value);
			return _builder;
		}

		public TBuilder ToRegistered(string objectId)
		{
			_insertCtorArgAction(_holder.Definition.ConstructorArgumentValues, new RuntimeObjectReference(objectId));
			return _builder;
		}

		public TBuilder ToRegistered<TRef>(ObjectRef<TRef> reference) where TRef : TArgument
		{
			return ToRegistered(reference.Id);
		}

		public TBuilder ToRegisteredDefault()
		{
			return ToRegistered(IdGenerator<TArgument>.GetDefaultId());
		}

		public TBuilder ToRegisteredDefaultOf<TReferencedType>() where TReferencedType : TArgument
		{
			return ToRegistered(IdGenerator<TReferencedType>.GetDefaultId());
		}

		public TBuilder ToInlineDefinition<TInnerObject>() where TInnerObject : TArgument
		{
			_insertCtorArgAction(
				_holder.Definition.ConstructorArgumentValues,
				new ObjectDefinitionBuilder<TInnerObject>(null).Definition);

			return _builder;
		}

		public TBuilder ToInlineDefinition<TInnerObject>(Action<IInstantiationBuildStage<TInnerObject>> innerObjectBuildAction) where TInnerObject : TArgument
		{
			var innerObjectBuilder = new ObjectDefinitionBuilder<TInnerObject>(null);
			innerObjectBuildAction(innerObjectBuilder);
			_insertCtorArgAction(_holder.Definition.ConstructorArgumentValues, innerObjectBuilder.Definition);
			return _builder;
		}
	}
}