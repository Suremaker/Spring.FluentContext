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
using Spring.FluentContext.BuildingStages.Objects;

namespace Spring.FluentContext.Impl
{
	internal class FactoryMethodArgDefinitionBuilder<TObject, TArg1> : IFactoryMethodArgBuildStage<TObject, TArg1>
	{
		private readonly ObjectDefinitionBuilder<TObject> _holder;

		public FactoryMethodArgDefinitionBuilder(ObjectDefinitionBuilder<TObject> holder)
		{
			_holder = holder;
		}

		public IMethodArgumentDefinitionBuilder<IAutoConfigurationBuildStage<TObject>, TArg1> BindMethodArg()
		{
			return new MethodArgumentDefinitionBuilder<IAutoConfigurationBuildStage<TObject>, TArg1>(_holder, _holder);
		}
	}

	internal class FactoryMethodArgDefinitionBuilder<TObject, TArg1, TArg2> : IFactoryMethodArgBuildStage<TObject, TArg1, TArg2>
	{
		private readonly ObjectDefinitionBuilder<TObject> _holder;

		public FactoryMethodArgDefinitionBuilder(ObjectDefinitionBuilder<TObject> holder)
		{
			_holder = holder;
		}

		public IMethodArgumentDefinitionBuilder<IFactoryMethodArgBuildStage<TObject, TArg2>, TArg1> BindMethodArg()
		{
			return new MethodArgumentDefinitionBuilder<IFactoryMethodArgBuildStage<TObject, TArg2>, TArg1>(_holder, new FactoryMethodArgDefinitionBuilder<TObject, TArg2>(_holder));
		}
	}

	internal class FactoryMethodArgDefinitionBuilder<TObject, TArg1, TArg2, TArg3> : IFactoryMethodArgBuildStage<TObject, TArg1, TArg2, TArg3>
	{
		private readonly ObjectDefinitionBuilder<TObject> _holder;

		public FactoryMethodArgDefinitionBuilder(ObjectDefinitionBuilder<TObject> holder)
		{
			_holder = holder;
		}

		public IMethodArgumentDefinitionBuilder<IFactoryMethodArgBuildStage<TObject, TArg2, TArg3>, TArg1> BindMethodArg()
		{
			return new MethodArgumentDefinitionBuilder<IFactoryMethodArgBuildStage<TObject, TArg2, TArg3>, TArg1>(_holder, new FactoryMethodArgDefinitionBuilder<TObject, TArg2, TArg3>(_holder));
		}
	}
}