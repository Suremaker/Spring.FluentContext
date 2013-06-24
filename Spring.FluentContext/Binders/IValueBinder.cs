namespace Spring.FluentContext.Binders
{
	/// <summary>
	/// Interface for value binder.
	/// </summary>
	/// <typeparam name="TBuilder">Type of builder returned after binding is done.</typeparam>
	/// <typeparam name="TTargetType">Type of binding.</typeparam>
	public interface IValueBinder<out TBuilder, in TTargetType>
	{
		/// <summary>
		/// Specifies <c>value</c> as binding target.
		/// </summary>
		/// <param name="value">Value to bind.</param>
		/// <returns>builder.</returns>
		TBuilder ToValue(TTargetType value);
	}
}