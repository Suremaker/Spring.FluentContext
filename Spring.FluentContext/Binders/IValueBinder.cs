namespace Spring.FluentContext.Binders
{
	public interface IValueBinder<out TBuilder, in TTargetType>
	{
		TBuilder ToValue(TTargetType value);
	}
}