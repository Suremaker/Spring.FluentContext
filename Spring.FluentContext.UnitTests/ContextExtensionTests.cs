using NUnit.Framework;
using Spring.Context.Support;
using Spring.FluentContext.UnitTests.TestTypes;
using Spring.Objects.Factory;

namespace Spring.FluentContext.UnitTests
{
	[TestFixture]
	public class ContextExtensionTests
	{
		[Test]
		public void Get_named_object()
		{
			var ctx = new StaticApplicationContext();
			ctx.ObjectFactory.RegisterSingleton("test", new SimpleType());

			Assert.That(ctx.GetObject<SimpleType>("test"), Is.Not.Null);
		}

		[Test]
		public void Throw_on_get_object_by_wrong_type()
		{
			var ctx = new StaticApplicationContext();
			ctx.ObjectFactory.RegisterSingleton("test", new SimpleType());

			Assert.Throws<ObjectNotOfRequiredTypeException>(() => ctx.GetObject<string>("test"));
		}
	}
}
