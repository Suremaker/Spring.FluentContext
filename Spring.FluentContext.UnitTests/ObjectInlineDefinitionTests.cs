using NUnit.Framework;
using Spring.FluentContext.UnitTests.TestTypes;

namespace Spring.FluentContext.UnitTests
{
	[TestFixture]
	public class ObjectInlineDefinitionTests
	{
		private FluentApplicationContext _ctx;

		[SetUp]
		public void SetUp()
		{
			_ctx = new FluentApplicationContext();
		}

		[Test]
		public void Inline_defined_object_are_always_prototypes()
		{
			_ctx.RegisterDefault<NestingType>()
				.AsPrototype()
				.BindProperty(n => n.Simple).ToInlineDefinition<SimpleType>(def => { });

			var nesting1 = _ctx.GetObject<NestingType>();
			var nesting2 = _ctx.GetObject<NestingType>();

			Assert.That(nesting1.Simple, Is.Not.SameAs(nesting2.Simple));
		}
	}
}
