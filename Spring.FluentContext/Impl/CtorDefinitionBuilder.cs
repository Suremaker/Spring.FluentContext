using Spring.FluentContext.Builders;
using Spring.FluentContext.BuildingStages.Objects;

namespace Spring.FluentContext.Impl
{
	internal class GenericCtorDefinitionBuilder<TBuilder, TArgument>
	{
		protected IDefinitionHolder Holder { private get; set; }

		protected TBuilder Builder { private get; set; }

		public IMethodArgumentDefinitionBuilder<TBuilder, TArgument> BindConstructorArg()
		{
			return new MethodArgumentDefinitionBuilder<TBuilder,TArgument>(Holder, Builder);
		}
	}

	internal class CtorDefinitionBuilder<TObject, TArg>
		: GenericCtorDefinitionBuilder<IMethodConfigurationBuildStage<TObject>, TArg>, 
		ICtorDefinitionBuildStage<TObject, TArg>
	{
		public CtorDefinitionBuilder(ObjectDefinitionBuilder<TObject> builder)
		{
			Holder = builder;
			Builder = builder;
		}
	}

	internal class CtorDefinitionBuilder<TObject, TArg1, TArg2> 
		: GenericCtorDefinitionBuilder<ICtorDefinitionBuildStage<TObject, TArg2>, TArg1>, 
		ICtorDefinitionBuildStage<TObject, TArg1, TArg2>
	{
		public CtorDefinitionBuilder(ObjectDefinitionBuilder<TObject> builder)
		{
			Holder = builder;
			Builder = new CtorDefinitionBuilder<TObject,TArg2>(builder);
		}
	}

	internal class CtorDefinitionBuilder<TObject, TArg1, TArg2, TArg3> 
		: GenericCtorDefinitionBuilder<ICtorDefinitionBuildStage<TObject, TArg2, TArg3>, TArg1>, 
		ICtorDefinitionBuildStage<TObject, TArg1, TArg2, TArg3>
	{
		public CtorDefinitionBuilder(ObjectDefinitionBuilder<TObject> builder)
		{
			Holder = builder;
			Builder = new CtorDefinitionBuilder<TObject,TArg2,TArg3>(builder);
		}
	}

	internal class CtorDefinitionBuilder<TObject, TArg1, TArg2, TArg3, TArg4> 
		: GenericCtorDefinitionBuilder<ICtorDefinitionBuildStage<TObject, TArg2, TArg3, TArg4>, TArg1>, 
		ICtorDefinitionBuildStage<TObject, TArg1, TArg2, TArg3, TArg4>
	{
		public CtorDefinitionBuilder(ObjectDefinitionBuilder<TObject> builder)
		{
			Holder = builder;
			Builder = new CtorDefinitionBuilder<TObject,TArg2,TArg3,TArg4>(builder);
		}
	}
}
