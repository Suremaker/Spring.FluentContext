using NUnit.Framework;
using Spring.FluentContext.UnitTests.TestTypes;

namespace Spring.FluentContext.UnitTests
{
	[TestFixture]
	public class ObjectRegistrationTests
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
		public void Register_default_object()
		{
			_ctx.Register<SimpleType>();
			Assert.That(_ctx.GetObject<SimpleType>(), Is.Not.Null);
		}

		[Test]
		public void Register_named_object_and_get_its_ref()
		{
			var reference = _ctx.Register<SimpleType>("test").GetReference();
			Assert.That(reference.Id, Is.EqualTo("test"));
		}

		[Test]
		public void Register_default_object_and_get_its_ref()
		{
			var reference = _ctx.Register<SimpleType>().GetReference();

			var actual = _ctx.GetObject<SimpleType>(reference.Id);
			var expected = _ctx.GetObject<SimpleType>();
			Assert.That(actual, Is.SameAs(expected));
		}
	}
}
