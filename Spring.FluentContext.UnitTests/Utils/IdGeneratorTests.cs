using NUnit.Framework;
using Spring.FluentContext.UnitTests.TestTypes;
using Spring.FluentContext.Utils;

namespace Spring.FluentContext.UnitTests.Utils
{
	[TestFixture]
	public class IdGeneratorTests
	{
		[Test]
		public void Get_default_id_returns_same_value_for_same_type()
		{
			var id1 = IdGenerator<SimpleType>.GetDefaultId();
			var id2 = IdGenerator<SimpleType>.GetDefaultId();

			Assert.That(id1, Is.EqualTo(id2));
		}

		[Test]
		public void Get_default_id_returns_different_value_for_different_types()
		{
			var id1 = IdGenerator<SimpleType>.GetDefaultId();
			var id2 = IdGenerator<OtherType>.GetDefaultId();

			Assert.That(id1, Is.Not.EqualTo(id2));
		}

		[Test]
		public void Get_unique_id_always_returns_different_value()
		{
			var id1 = IdGenerator<SimpleType>.GetUniqueId();
			var id2 = IdGenerator<SimpleType>.GetUniqueId();

			Assert.That(id1, Is.Not.EqualTo(id2));
		}
	}
}
