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

		public IReferencingStage<TObject> ToRegistered(IObjectRef<TObject> reference)
		{
			return MakeAlias(reference.Id);
		}

		public IObjectRef<TObject> GetReference()
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