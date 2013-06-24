using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Spring.FluentContext.Builders;
using Spring.FluentContext.BuildingStages.Objects;
using Spring.FluentContext.Impl;
using Spring.FluentContext.Utils;
using Spring.Objects.Factory.Config;
using Spring.Objects.Factory.Support;

namespace Spring.FluentContext.Definitions
{
	/// <summary>
	/// Class allowing to create various object definitions.
	/// </summary>
	public class Def
	{
		/// <summary>
		/// Creates object definition using <c>objectBuildAction</c>.
		/// </summary>
		/// <typeparam name="TObject">Type of defined object.</typeparam>
		/// <param name="objectBuildAction">Action used to configure object.</param>
		/// <returns>Definition.</returns>
		public static IDefinition<TObject> Object<TObject>(Action<IInstantiationBuildStage<TObject>> objectBuildAction)
		{
			var builder = new ObjectDefinitionBuilder<TObject>(null);
			objectBuildAction(builder);
			return new Definition<TObject>(builder.Definition);
		}

		/// <summary>
		/// Creates default, inline object definition.
		/// </summary>
		/// <typeparam name="TObject">Type of defined object.</typeparam>
		/// <returns>Definition.</returns>
		public static IDefinition<TObject> Object<TObject>()
		{
			var builder = new ObjectDefinitionBuilder<TObject>(null);
			return new Definition<TObject>(builder.Definition);
		}

		/// <summary>
		/// Creates definition referencing to object of <c>TTargetType</c> type with default id.
		/// </summary>
		/// <typeparam name="TTargetType">Type of referenced object.</typeparam>
		/// <returns>Definition.</returns>
		public static IObjectRef<TTargetType> Reference<TTargetType>()
		{
			return Reference<TTargetType>(IdGenerator<TTargetType>.GetDefaultId());
		}

		/// <summary>
		/// Creates definition referencing to object of <c>TTargetType</c> type with <c>objectId</c> id.
		/// </summary>
		/// <typeparam name="TTargetType">Type of referenced object.</typeparam>
		/// <param name="objectId">Id of referenced object</param>
		/// <returns>Definition.</returns>
		public static IObjectRef<TTargetType> Reference<TTargetType>(string objectId)
		{
			return new ObjectRef<TTargetType>(objectId);
		}

		/// <summary>
		/// Creates definition of constant <c>value</c> of <c>TValue</c> type.
		/// </summary>
		/// <typeparam name="TValue">Type of constant value.</typeparam>
		/// <param name="value">Value.</param>
		/// <returns>Definition.</returns>
		public static IDefinition<TValue> Value<TValue>(TValue value)
		{
			return new Definition<TValue>(value);
		}

		/// <summary>
		/// Creates definition of array of <c>TElement</c> type, containing elements described by <c>items</c> definitions.
		/// </summary>
		/// <typeparam name="TElement">Array item type.</typeparam>
		/// <param name="items">Definitions of array items.</param>
		/// <returns>Definition.</returns>
		public static IDefinition<TElement[]> Array<TElement>(params IDefinition<TElement>[] items)
		{
			return new Definition<TElement[]>(ToList(items));
		}

		/// <summary>
		/// Creates definition of list of <c>TElement</c> type, containing elements described by <c>items</c> definitions.
		/// </summary>
		/// <typeparam name="TElement">List item type.</typeparam>
		/// <param name="items">Definitions of list items.</param>
		/// <returns>Definition.</returns>
		public static IDefinition<List<TElement>> List<TElement>(params IDefinition<TElement>[] items)
		{
			return new Definition<List<TElement>>(ToList(items));
		}

		/// <summary>
		/// Creates definition of dictionary, where items are filled by <c>dictionaryBuilder</c> action.
		/// </summary>
		/// <typeparam name="TKey">Dictionary key type.</typeparam>
		/// <typeparam name="TValue">Dictionary value type.</typeparam>
		/// <param name="dictionaryBuilder">Dictionary builder action filling constructed dictionary with key-value definitions.</param>
		/// <returns>Definition.</returns>
		public static IDefinition<Dictionary<TKey, TValue>> Dictionary<TKey, TValue>(Action<IDictionaryDefinitionBuilder<TKey, TValue>> dictionaryBuilder)
		{
			var builder = new DictionaryDefinitionBuilder<TKey, TValue>();
			dictionaryBuilder(builder);
			return new Definition<Dictionary<TKey, TValue>>(builder.Dictionary);
		}

		/// <summary>
		/// Creates definition of property retriever that returns property value of <c>target</c> object, where property is specified by <c>propertySelector</c>.
		/// </summary>
		/// <typeparam name="TTargetObject">Type of target object.</typeparam>
		/// <typeparam name="TPropertyType">Type of property value.</typeparam>
		/// <param name="target">Target object which property should be accessed.</param>
		/// <param name="propertySelector">Property selector.</param>
		/// <returns>Property retriever definition.</returns>
		public static IDefinition<TPropertyType> ObjectProperty<TTargetObject, TPropertyType>(IDefinition<TTargetObject> target, Expression<Func<TTargetObject, TPropertyType>> propertySelector)
		{
			var factoryDef = new GenericObjectDefinition { ObjectType = typeof(PropertyRetrievingFactoryObject) };
			factoryDef.PropertyValues.Add("TargetObject", target.DefinitionObject);
			factoryDef.PropertyValues.Add("TargetProperty", ReflectionUtils.GetPropertyPath(propertySelector));
			return new Definition<TPropertyType>(factoryDef);
		}

		private static IManagedCollection ToList<TTargetType>(IEnumerable<IDefinition<TTargetType>> items)
		{
			var list = new ManagedList { ElementTypeName = typeof(TTargetType).FullName };
			foreach (var def in items.Select(i => i.DefinitionObject))
				list.Add(def);
			return list;
		}
	}
}
