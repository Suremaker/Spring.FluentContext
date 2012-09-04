namespace Spring.FluentContext
{
	public interface ICtorDefinitionBuilder<TObject, in TProperty>
	{
		IObjectDefinitionBuilder<TObject> ToValue(TProperty value);
		IObjectDefinitionBuilder<TObject> ToReference(string objectId);
	}
}