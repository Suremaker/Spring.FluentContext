namespace Spring.FluentContext
{
	public interface ILookupMethodDefinitionBuilder<TObject, in TResult>
	{
		IObjectDefinitionBuilder<TObject> ToReference(string objectId);
	}
}