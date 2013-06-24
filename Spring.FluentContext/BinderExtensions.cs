using System;
using System.Collections.Generic;
using Spring.FluentContext.Binders;
using Spring.FluentContext.Builders;
using Spring.FluentContext.Definitions;

namespace Spring.FluentContext
{
	/// <summary>
	/// Binder extension class helping with binding constructor / property values to definitions such as collections
	/// </summary>
	public static class BinderExtensions
	{
		/// <summary>
		/// Allows to bind given <c>values</c> to constructor argument or property value defined as a list.
		/// </summary>
		/// <typeparam name="TBuilder">Result builder.</typeparam>
		/// <typeparam name="TElement">Type of collection elements.</typeparam>
		/// <param name="binder">Binder.</param>
		/// <param name="values">Collection values.</param>
		/// <returns>Builder.</returns>
		public static TBuilder ToList<TBuilder, TElement>(this IDefinitionBinder<TBuilder,List<TElement>> binder, params IDefinition<TElement>[] values)
		{
			return binder.To(Def.List(values));
		}

		/// <summary>
		/// Allows to bind given <c>values</c> to constructor argument or property value defined as an array.
		/// </summary>
		/// <typeparam name="TBuilder">Result builder.</typeparam>
		/// <typeparam name="TElement">Type of collection elements.</typeparam>
		/// <param name="binder">Binder.</param>
		/// <param name="values">Collection values.</param>
		/// <returns>Builder.</returns>
		public static TBuilder ToArray<TBuilder, TElement>(this IDefinitionBinder<TBuilder, TElement[]> binder, params IDefinition<TElement>[] values)
		{
			return binder.To(Def.Array(values));
		}

		/// <summary>
		/// Allows to bind dictionary configured by given <c>dictionaryBuilder</c> action to constructor argument or property value.
		/// </summary>
		/// <typeparam name="TBuilder">Result builder.</typeparam>
		/// <typeparam name="TKey">Dictionary key type.</typeparam>
		/// <typeparam name="TValue">Dictionary value type.</typeparam>
		/// <param name="binder">Binder.</param>
		/// <param name="dictionaryBuilder">Dictionary builder action filling constructed dictionary with key-value definitions.</param>
		/// <returns>Builder.</returns>
		public static TBuilder ToDictionary<TBuilder, TKey, TValue>(this IDefinitionBinder<TBuilder, Dictionary<TKey,TValue>> binder, Action<IDictionaryDefinitionBuilder<TKey, TValue>> dictionaryBuilder)
		{
			return binder.To(Def.Dictionary(dictionaryBuilder));
		}
	}
}
