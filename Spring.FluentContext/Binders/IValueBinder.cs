namespace Spring.FluentContext.Binders
{
	public interface IValueBinder<TObject, in TTargetType>
	{
		IObjectDefinitionBuilder<TObject> ToValue(TTargetType value);
	}
}