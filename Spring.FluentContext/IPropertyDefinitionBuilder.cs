namespace Spring.FluentContext
{
	public interface IPropertyDefinitionBuilder<TObject, in TProperty>
	{
		IObjectDefinitionBuilder<TObject> ToValue(TProperty value);
		IObjectDefinitionBuilder<TObject> ToReference(string objectId);
	}
}