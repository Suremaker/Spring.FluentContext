namespace Spring.FluentContext.Binders
{
	public interface IReferenceBinder<out TBuilder, in TTargetType>
	{
		TBuilder ToRegistered(string objectId);
		TBuilder ToRegistered<TReferencedType>(ObjectRef<TReferencedType> reference) where TReferencedType : TTargetType;
		TBuilder ToRegisteredDefaultOfType<TReferencedType>() where TReferencedType : TTargetType;
		TBuilder ToRegisteredDefault();
	}
}