using NUnit.Framework;

namespace Spring.FluentContext.UnitTests
{
	[TestFixture]
	[Ignore("Not implemented")]
	public class FluentApplicationContext
	{
		private FluentContext.FluentApplicationContext _ctx;

		[SetUp]
		public void SetUp()
		{
			_ctx = new FluentContext.FluentApplicationContext();
		}

		[Test]
		public void Register_named_object()
		{
			_ctx.Register<SimpleType>("test");

			Assert.That(_ctx.GetObject<SimpleType>("test"), Is.Not.Null);
		}
	}
}
