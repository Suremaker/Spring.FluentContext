using NUnit.Framework;
using Spring.FluentContext.UnitTests.TestTypes;

namespace Spring.FluentContext.UnitTests
{
	[TestFixture]
	public class LookupMethodInjectionTests
	{
		private FluentApplicationContext _ctx;

		[SetUp]
		public void SetUp()
		{
			_ctx = new FluentApplicationContext();
			CountingType.ClearCounter();
		}

		[Test]
		public void Bind_lookup_method_to_prototype()
		{
			_ctx.Register<TypeWithFactoryMethod>("test")
				.BindLookupMethod(t => t.CreateType(), "counting");

			_ctx.Register<CountingType>("counting").AsPrototype();

			var actual = _ctx.GetObject<TypeWithFactoryMethod>("test");

			var object1 = actual.CreateType();
			var object2 = actual.CreateType();

			Assert.That(object1.CurrentCount, Is.EqualTo(1));
			Assert.That(object2.CurrentCount, Is.EqualTo(2));
		}

		[Test]
		public void Bind_lookup_method_to_singleton()
		{
			_ctx.Register<TypeWithFactoryMethod>("test")
				.BindLookupMethod(t => t.CreateType(), "counting");

			_ctx.Register<CountingType>("counting").AsSingleton();

			var actual = _ctx.GetObject<TypeWithFactoryMethod>("test");

			var object1 = actual.CreateType();
			var object2 = actual.CreateType();

			Assert.That(object1.CurrentCount, Is.EqualTo(1));
			Assert.That(object2.CurrentCount, Is.EqualTo(1));
		}
	}
}
