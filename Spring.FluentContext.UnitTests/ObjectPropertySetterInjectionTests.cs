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

			_ctx.RegisterNamed<SimpleType>("test").AsSingleton()
				.BindProperty(t => t.Text).ToValue(expectedText);

			var actual = _ctx.GetObject<SimpleType>("test");
			Assert.That(actual.Text, Is.EqualTo(expectedText));
		}

		[Test]
		public void Bind_multiple_properties_in_chain()
		{
			const string expectedText = "some value";
			const int expectedValue = 10;

			_ctx.RegisterNamed<SimpleType>("test")
				.BindProperty(t => t.Text).ToValue(expectedText)
				.BindProperty(t => t.Value).ToValue(expectedValue);

			var actual = _ctx.GetObject<SimpleType>("test");

			Assert.That(actual.Text, Is.EqualTo(expectedText));
			Assert.That(actual.Value, Is.EqualTo(expectedValue));
		}

		[Test]
		public void Bind_property_to_other_object()
		{
			_ctx.RegisterNamed<SimpleType>("simple");

			_ctx.RegisterNamed<NestingType>("nesting")
				.BindProperty(n => n.Simple).ToRegistered("simple");

			var actual = _ctx.GetObject<NestingType>("nesting");
			Assert.That(actual.Simple, Is.SameAs(_ctx.GetObject<SimpleType>("simple")));
		}

		[Test]
		public void Bind_property_to_reference_of_derived_object()
		{
			var derivedRef = _ctx.RegisterNamed<DerivedFromSimpleType>("derived").GetReference();
			_ctx.RegisterDefault<NestingType>()
				.BindProperty(n => n.Simple).ToRegistered(derivedRef);

			Assert.That(_ctx.GetObject<NestingType>().Simple, Is.TypeOf<DerivedFromSimpleType>());
		}

		[Test]
		public void Bind_property_to_default_reference_of_derived_object()
		{
			_ctx.RegisterDefault<DerivedFromSimpleType>();
			_ctx.RegisterDefault<NestingType>()
				.BindProperty(n => n.Simple).ToRegisteredDefaultOfType<DerivedFromSimpleType>();

			Assert.That(_ctx.GetObject<NestingType>().Simple, Is.TypeOf<DerivedFromSimpleType>());
		}

		[Test]
		public void Bind_property_to_other_object_by_ref()
		{
			var simpleRef = _ctx.RegisterNamed<SimpleType>("simple").GetReference();

			_ctx.RegisterNamed<NestingType>("nesting")
				.BindProperty(n => n.Simple).ToRegistered(simpleRef);

			var actual = _ctx.GetObject<NestingType>("nesting");
			Assert.That(actual.Simple, Is.SameAs(_ctx.GetObject<SimpleType>("simple")));
		}

		[Test]
		public void Bind_property_to_inner_object()
		{
			const string expectedText = "some text";

			_ctx.RegisterNamed<NestingType>("nesting")
				.BindProperty(n => n.Simple).ToInlineDefinition<SimpleType>(
					def => def.BindProperty(s => s.Text).ToValue(expectedText));

			var actual = _ctx.GetObject<NestingType>("nesting");
			Assert.That(actual.Simple.Text, Is.EqualTo(expectedText));
		}

		[Test]
		public void Bind_property_to_default_ref()
		{
			_ctx.RegisterDefault<SimpleType>();
			_ctx.RegisterDefault<OtherType>();
			_ctx.RegisterDefault<NestingType>()
				.BindProperty(t => t.Other).ToRegisteredDefault()
				.BindProperty(t => t.Simple).ToRegisteredDefault();

			var actual = _ctx.GetObject<NestingType>();
			Assert.That(actual.Simple, Is.SameAs(_ctx.GetObject<SimpleType>()));
			Assert.That(actual.Other, Is.SameAs(_ctx.GetObject<OtherType>()));
		}

		[Test]
		public void Bind_set_only_property()
		{
			const string expected = "some text";
			_ctx.RegisterNamed<IocType>("test")
				.BindPropertyNamed<string>("Text").ToValue(expected);

			var actual = _ctx.GetObject<IocType>("test");
			Assert.That(actual.ToString(), Is.EqualTo(expected));
		}
	}
}
