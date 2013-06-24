using Spring.FluentContext.Binders;

namespace Spring.FluentContext.Builders
{
	/// <summary>
	/// Interface for constructor argument definition builder.
	/// </summary>
	/// <typeparam name="TBuilder">Type of builder instance returned when constructor argument building is finished.</typeparam>
	/// <typeparam name="TArgument">Type of constructor argument.</typeparam>
	public interface IMethodArgumentDefinitionBuilder<TBuilder, in TArgument>
		: IReferenceBinder<TBuilder, TArgument>,
		IInlineDefinitionBinder<TBuilder, TArgument>,
		IValueBinder<TBuilder, TArgument>,
		IDefinitionBinder<TBuilder, TArgument>
	{
	}
}