using Spring.FluentContext.Binders;

namespace Spring.FluentContext
{
	public interface ICtorDefinitionBuilder<TObject, in TArgument>
		: IReferenceBinder<TObject, TArgument>,
		IInlineDefinitionBinder<TObject, TArgument>,
		IValueBinder<TObject, TArgument>
	{
	}
}