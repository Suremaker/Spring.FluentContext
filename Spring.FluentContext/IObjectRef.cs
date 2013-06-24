using Spring.FluentContext.Definitions;

namespace Spring.FluentContext
{
	/// <summary>
	/// Interface for object reference.
	/// </summary>
	/// <typeparam name="T">Type of referenced object.</typeparam>
	public interface IObjectRef<out T> : IDefinition<T>
	{
		/// <summary>
		/// Object id.
		/// </summary>
		string Id { get; }
	}
}