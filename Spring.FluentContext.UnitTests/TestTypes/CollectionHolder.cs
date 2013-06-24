using System.Collections.Generic;

namespace Spring.FluentContext.UnitTests.TestTypes
{
	class CollectionHolder
	{
		public SimpleType[] Array { get; set; }
		public IList<OtherType> List { get; set; }
		public IEnumerable<DerivedFromSimpleType> Collection { get; set; }
		public IDictionary<int, SimpleType> Dictionary { get; set; } 

		public CollectionHolder() { }

		public CollectionHolder(IDictionary<int, SimpleType> values)
		{
			Dictionary = values;
		}

		public CollectionHolder(SimpleType[] values)
		{
			Array = values;
		}
		
		public CollectionHolder(IList<OtherType> values)
		{
			List = values;
		}

		public CollectionHolder(IEnumerable<DerivedFromSimpleType> values)
		{
			Collection = values;
		}

	}
}
