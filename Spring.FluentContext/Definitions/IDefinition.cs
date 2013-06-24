namespace Spring.FluentContext.Definitions
{
	/// <summary>
	/// Interface for object definitions.
	/// </summary>
	/// <typeparam name="TTargetType">Type of defined object.</typeparam>
	public interface IDefinition<out TTargetType>
	{
		/// <summary>
		/// Internal definition object.
		/// </summary>
		object DefinitionObject { get; }
	}
}
