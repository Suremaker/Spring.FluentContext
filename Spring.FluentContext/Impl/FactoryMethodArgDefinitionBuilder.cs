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