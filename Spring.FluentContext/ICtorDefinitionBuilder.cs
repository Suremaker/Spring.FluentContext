namespace Spring.FluentContext
{
	public interface ICtorDefinitionBuilder<TObject, in TProperty>
	{
		IObjectDefinitionBuilder<TObject> ToValue(TProperty value);
	}
}