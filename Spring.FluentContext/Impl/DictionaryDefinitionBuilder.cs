using Spring.FluentContext.Builders;
using Spring.FluentContext.Definitions;
using Spring.Objects.Factory.Config;

namespace Spring.FluentContext.Impl
{
	internal class DictionaryDefinitionBuilder<TKey, TValue> : IDictionaryDefinitionBuilder<TKey, TValue>
	{
		private readonly ManagedDictionary _dictionary = new ManagedDictionary
		{
			KeyTypeName = typeof(TKey).FullName,
			ValueTypeName = typeof(TValue).FullName
		};

		public IDictionaryDefinitionBuilder<TKey, TValue> Set(IDefinition<TKey> key, IDefinition<TValue> value)
		{
			_dictionary[key.DefinitionObject] = value.DefinitionObject;
			return this;
		}

		public IDefinition<TValue> this[IDefinition<TKey> key]
		{
			set { Set(key, value); }
		}

		public ManagedDictionary Dictionary
		{
			get { return _dictionary; }
		}
	}
}