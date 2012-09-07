using Spring.FluentContext.Binders;

namespace Spring.FluentContext.Builders
{
	public interface ICtorArgumentDefinitionBuilder<TBuilder, in TArgument>
		: IReferenceBinder<TBuilder, TArgument>,
		IInlineDefinitionBinder<TBuilder, TArgument>,
		IValueBinder<TBuilder, TArgument>
	{
	}
}