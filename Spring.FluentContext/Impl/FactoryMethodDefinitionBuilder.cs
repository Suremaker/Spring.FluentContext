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
using Spring.FluentContext.BuildingStages.Objects;
using Spring.FluentContext.Utils;

namespace Spring.FluentContext.Impl
{
	internal class GenericFactoryMethodDefinitionBuilder<TBuilder, TFactory, TObject>
	{
		private readonly ObjectDefinitionBuilder<TObject> _holder;
		private readonly TBuilder _builder;

		public GenericFactoryMethodDefinitionBuilder(ObjectDefinitionBuilder<TObject> holder, TBuilder builder)
		{
			_holder = holder;
			_builder = builder;
		}

		public TBuilder OfRegisteredDefault()
		{
			return OfRegistered(IdGenerator<TFactory>.GetDefaultId());
		}

		public TBuilder OfRegistered(string objectId)
		{
			_holder.Definition.FactoryObjectName = objectId;
			return _builder;
		}

		public TBuilder OfRegistered(IObjectRef<TFactory> reference)
		{
			return OfRegistered(reference.Id);
		}
	}

	internal class FactoryMethodDefinitionBuilder<TFactory, TObject> : GenericFactoryMethodDefinitionBuilder<IAutoConfigurationBuildStage<TObject>, TFactory, TObject>, IFactoryMethodDefinitionBuilder<TFactory, TObject>
	{
		public FactoryMethodDefinitionBuilder(ObjectDefinitionBuilder<TObject> holder)
			: base(holder, holder)
		{
		}
	}

	internal class FactoryMethodDefinitionBuilder<TFactory, TObject, TArg1> : GenericFactoryMethodDefinitionBuilder<IFactoryMethodArgBuildStage<TObject, TArg1>, TFactory, TObject>, IFactoryMethodDefinitionBuilder<TFactory, TObject, TArg1>
	{
		public FactoryMethodDefinitionBuilder(ObjectDefinitionBuilder<TObject> holder)
			: base(holder, new FactoryMethodArgDefinitionBuilder<TObject, TArg1>(holder))
		{
		}
	}

	internal class FactoryMethodDefinitionBuilder<TFactory, TObject, TArg1, TArg2> : GenericFactoryMethodDefinitionBuilder<IFactoryMethodArgBuildStage<TObject, TArg1, TArg2>, TFactory, TObject>, IFactoryMethodDefinitionBuilder<TFactory, TObject, TArg1, TArg2>
	{
		public FactoryMethodDefinitionBuilder(ObjectDefinitionBuilder<TObject> holder)
			: base(holder, new FactoryMethodArgDefinitionBuilder<TObject, TArg1, TArg2>(holder))
		{
		}
	}

	internal class FactoryMethodDefinitionBuilder<TFactory, TObject, TArg1, TArg2, TArg3> : GenericFactoryMethodDefinitionBuilder<IFactoryMethodArgBuildStage<TObject, TArg1, TArg2, TArg3>, TFactory, TObject>, IFactoryMethodDefinitionBuilder<TFactory, TObject, TArg1, TArg2, TArg3>
	{
		public FactoryMethodDefinitionBuilder(ObjectDefinitionBuilder<TObject> holder)
			: base(holder, new FactoryMethodArgDefinitionBuilder<TObject, TArg1, TArg2, TArg3>(holder))
		{
		}
	}
}