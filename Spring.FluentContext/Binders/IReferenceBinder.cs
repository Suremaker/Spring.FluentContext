namespace Spring.FluentContext.Binders
{
	public interface IReferenceBinder<TObject, in TTargetType>
	{
		IObjectDefinitionBuilder<TObject> ToRegistered(string objectId);
		IObjectDefinitionBuilder<TObject> ToRegistered<TReferencedType>(ObjectRef<TReferencedType> reference) where TReferencedType : TTargetType;
		IObjectDefinitionBuilder<TObject> ToRegisteredDefaultOfType<TReferencedType>() where TReferencedType : TTargetType;
		IObjectDefinitionBuilder<TObject> ToRegisteredDefault();
	}
}