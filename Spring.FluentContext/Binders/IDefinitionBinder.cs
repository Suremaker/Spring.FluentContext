using Spring.FluentContext.Definitions;

namespace Spring.FluentContext.Binders
{
	/// <summary>
	/// Interface for definition binder.
	/// </summary>
	/// <typeparam name="TBuilder">Type of builder returned after binding is done.</typeparam>
	/// <typeparam name="TTargetType">Type of binding.</typeparam>
	public interface IDefinitionBinder<out TBuilder, in TTargetType>
	{
		/// <summary>
		/// Specifies object defined by <c>definition</c> as binding target.
		/// To see available definition builder classes, see classes with <see cref="Spring.FluentContext.Definitions"/> namespace.
		/// </summary>
		/// <param name="definition">Definition of object that would be bound.</param>
		/// <returns>Builder.</returns>
		TBuilder To(IDefinition<TTargetType> definition);
	}
}