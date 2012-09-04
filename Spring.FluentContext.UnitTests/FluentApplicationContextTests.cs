using NUnit.Framework;
using Spring.FluentContext.UnitTests.TestTypes;

namespace Spring.FluentContext.UnitTests
{
	[TestFixture]
	public class FluentApplicationContextTests
	{
		private FluentApplicationContext _ctx;

		[SetUp]
		public void SetUp()
		{
			_ctx = new FluentApplicationContext();
		}

		[Test]
		public void Register_named_object()
		{
			_ctx.Register<SimpleType>("test");

			Assert.That(_ctx.GetObject<SimpleType>("test"), Is.Not.Null);
		}

		[Test]
		public void Register_named_object_registers_it_as_singleton_by_default()
		{
			_ctx.Register<SimpleType>("test");

			Assert.That(
				_ctx.GetObject<SimpleType>("test"),
				Is.SameAs(_ctx.GetObject<SimpleType>("test")));
		}

		[Test]
		public void Register_named_object_as_prototype()
		{
			_ctx.Register<SimpleType>("test").AsPrototype();

			Assert.That(
				_ctx.GetObject<SimpleType>("test"),
				Is.Not.SameAs(_ctx.GetObject<SimpleType>("test")));
		}

		[Test]
		public void Register_named_object_as_singleton()
		{
			_ctx.Register<SimpleType>("test").AsSingleton();

			Assert.That(
				_ctx.GetObject<SimpleType>("test"),
				Is.SameAs(_ctx.GetObject<SimpleType>("test")));
		}

		[Test]
		public void Register_named_object_does_not_instantiate_it_until_accessed()
		{
			int beforeRegister = CountingType.Count;
			_ctx.Register<CountingType>("test");
			int beforeInstantiation = CountingType.Count;
			_ctx.GetObject<CountingType>("test");
			int afterInstantiation = CountingType.Count;

			Assert.That(beforeInstantiation, Is.EqualTo(beforeRegister));

			Assert.That(afterInstantiation, Is.EqualTo(beforeInstantiation + 1));
		}
	}
}
