using NUnit.Framework;
using Spring.FluentContext.UnitTests.TestTypes;

namespace Spring.FluentContext.UnitTests
{
	[TestFixture]
	public class ObjectScopeTests
	{
		private FluentApplicationContext _ctx;

		[SetUp]
		public void SetUp()
		{
			_ctx = new FluentApplicationContext();
		}

		[Test]
		public void Register_named_object_registers_it_as_singleton_by_default()
		{
			_ctx.RegisterNamed<SimpleType>("test");

			Assert.That(
				_ctx.GetObject<SimpleType>("test"),
				Is.SameAs(_ctx.GetObject<SimpleType>("test")));
		}

		[Test]
		public void Register_named_object_as_prototype()
		{
			_ctx.RegisterNamed<SimpleType>("test").AsPrototype();

			Assert.That(
				_ctx.GetObject<SimpleType>("test"),
				Is.Not.SameAs(_ctx.GetObject<SimpleType>("test")));
		}

		[Test]
		public void Register_named_object_as_singleton()
		{
			_ctx.RegisterNamed<SimpleType>("test").AsSingleton();

			Assert.That(
				_ctx.GetObject<SimpleType>("test"),
				Is.SameAs(_ctx.GetObject<SimpleType>("test")));
		}
	}
}
