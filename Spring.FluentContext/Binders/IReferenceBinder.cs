namespace Spring.FluentContext.Binders
{
	public interface IReferenceBinder<TObject, in TTargetType>
	{
		IObjectDefinitionBuilder<TObject> ToReference(string objectId);
		IObjectDefinitionBuilder<TObject> ToReference<TReferencedType>(ObjectRef<TReferencedType> reference) where TReferencedType : TTargetType;
		IObjectDefinitionBuilder<TObject> ToDefaultReferenceOfType<TReferencedType>() where TReferencedType : TTargetType;
		IObjectDefinitionBuilder<TObject> ToDefaultReference();
	}
}