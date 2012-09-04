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
	}
}
