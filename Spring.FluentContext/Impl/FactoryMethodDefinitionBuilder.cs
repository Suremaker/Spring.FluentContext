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