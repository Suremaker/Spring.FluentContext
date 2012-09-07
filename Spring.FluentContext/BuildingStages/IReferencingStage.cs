namespace Spring.FluentContext.BuildingStages
{
	public interface IReferencingStage<TObject>
	{
		ObjectRef<TObject> GetReference();
	}
}
