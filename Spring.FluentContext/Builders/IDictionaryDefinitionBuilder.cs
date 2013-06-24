using Spring.FluentContext.Definitions;

namespace Spring.FluentContext.Builders
{
	/// <summary>
	/// Interface for dictionary definition builder.
	/// </summary>
	/// <typeparam name="TKey">Dictionary key type.</typeparam>
	/// <typeparam name="TValue">Dictionary value type.</typeparam>
	public interface IDictionaryDefinitionBuilder<in TKey, in TValue>
	{
		/// <summary>
		/// Adds key-value definition to dictionary. If value for given key already exists, it is overwritten.
		/// </summary>
		/// <param name="key">Key definition.</param>
		/// <param name="value">Value definition.</param>
		/// <returns>Builder.</returns>
		IDictionaryDefinitionBuilder<TKey, TValue> Set(IDefinition<TKey> key, IDefinition<TValue> value);

		/// <summary>
		/// Adds key-value definition to dictionary. If value for given key already exists, it is overwritten.
		/// </summary>
		/// <param name="key">Key definition.</param>
		IDefinition<TValue> this[IDefinition<TKey> key] { set; }
	}
}