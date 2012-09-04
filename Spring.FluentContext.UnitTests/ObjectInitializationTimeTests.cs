using NUnit.Framework;
using Spring.FluentContext.UnitTests.TestTypes;

namespace Spring.FluentContext.UnitTests
{
	[TestFixture]
	public class ObjectInitializationTimeTests
	{
		private FluentApplicationContext _ctx;

		[SetUp]
		public void SetUp()
		{
			_ctx = new FluentApplicationContext();
			CountingType.ClearCounter();
		}

		[Test]
		public void Register_named_object_does_not_instantiate_it_until_accessed()
		{
			_ctx.Register<CountingType>("test");
			
			int beforeInstantiation = CountingType.Count;
			_ctx.GetObject<CountingType>("test");
			int afterInstantiation = CountingType.Count;

			Assert.That(beforeInstantiation, Is.EqualTo(0));
			Assert.That(afterInstantiation, Is.EqualTo(1));
		}
	}
}