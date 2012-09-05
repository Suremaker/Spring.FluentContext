using NUnit.Framework;
using Spring.FluentContext.UnitTests.TestTypes;

namespace Spring.FluentContext.UnitTests
{
	[TestFixture]
	public class ObjectPropertySetterInjectionTests
	{
		private FluentApplicationContext _ctx;

		[SetUp]
		public void SetUp()
		{
			_ctx = new FluentApplicationContext();
		}

		[Test]
		public void Bind_property_to_value()
		{
			const string expectedText = "some value";

			_ctx.Register<SimpleType>("test").AsSingleton()
				.BindProperty(t => t.Text).ToValue(expectedText);

			var actual = _ctx.GetObject<SimpleType>("test");
			Assert.That(actual.Text, Is.EqualTo(expectedText));
		}

		[Test]
		public void Bind_multiple_properties_in_chain()
		{
			const string expectedText = "some value";
			const int expectedValue = 10;

			_ctx.Register<SimpleType>("test")
				.BindProperty(t => t.Text).ToValue(expectedText)
				.BindProperty(t => t.Value).ToValue(expectedValue);

			var actual = _ctx.GetObject<SimpleType>("test");

			Assert.That(actual.Text, Is.EqualTo(expectedText));
			Assert.That(actual.Value, Is.EqualTo(expectedValue));
		}

		[Test]
		public void Bind_property_to_other_object()
		{
			_ctx.Register<SimpleType>("simple");

			_ctx.Register<NestingType>("nesting")
				.BindProperty(n => n.Simple).ToReference("simple");

			var actual = _ctx.GetObject<NestingType>("nesting");
			Assert.That(actual.Simple, Is.SameAs(_ctx.GetObject<SimpleType>("simple")));
		}

		[Test]
		public void Bind_property_to_inner_object()
		{
			const string expectedText = "some text";

			_ctx.Register<NestingType>("nesting")
				.BindProperty(n => n.Simple).ToInlineDefinition<SimpleType>(
					def => def.BindProperty(s => s.Text).ToValue(expectedText));

			var actual = _ctx.GetObject<NestingType>("nesting");
			Assert.That(actual.Simple.Text, Is.EqualTo(expectedText));
		}

		[Test]
		public void Bind_property_to_default_ref()
		{
			_ctx.Register<SimpleType>();
			_ctx.Register<OtherType>();
			_ctx.Register<NestingType>()
				.BindProperty(t => t.Other).ToDefaultReference()
				.BindProperty(t => t.Simple).ToDefaultReference();

			var actual = _ctx.GetObject<NestingType>();
			Assert.That(actual.Simple, Is.SameAs(_ctx.GetObject<SimpleType>()));
			Assert.That(actual.Other, Is.SameAs(_ctx.GetObject<OtherType>()));
		}

		[Test]
		public void Bind_set_only_property()
		{
			const string expected = "some text";
			_ctx.Register<IocType>("test")
				.BindPropertyByName<string>("Text").ToValue(expected);

			var actual = _ctx.GetObject<IocType>("test");
			Assert.That(actual.ToString(), Is.EqualTo(expected));
		}
	}
}
