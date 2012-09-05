using NUnit.Framework;
using Spring.FluentContext.UnitTests.TestTypes;

namespace Spring.FluentContext.UnitTests
{
	[TestFixture]
	public class ObjectConstructorInjectionTests
	{
		private FluentApplicationContext _ctx;

		[SetUp]
		public void SetUp()
		{
			_ctx = new FluentApplicationContext();
		}

		[Test]
		public void Inject_values_by_ctor_using_indexed_constructor_arguments()
		{
			const string expectedText = "some text";
			const int expectedValue = 15;

			_ctx.Register<CtorHavingType>("test")
				.BindConstructorArg<string>(0).ToValue(expectedText)
				.BindConstructorArg<int>(1).ToValue(expectedValue);

			var actual = _ctx.GetObject<CtorHavingType>("test");

			Assert.That(actual.Text, Is.EqualTo(expectedText));
			Assert.That(actual.Value, Is.EqualTo(expectedValue));
		}

		[Test]
		public void Inject_values_by_ctor_using_indexed_constructor_arguments_and_proper_ctor()
		{
			const string expectedText = "some text";

			_ctx.Register<CtorHavingType>("test")
				.BindConstructorArg<string>(0).ToValue(expectedText);

			var actual = _ctx.GetObject<CtorHavingType>("test");

			Assert.That(actual.Text, Is.EqualTo(expectedText));
			Assert.That(actual.Value, Is.EqualTo(0));
		}

		[Test]
		public void Inject_values_by_ctor_using_generic_constructor_arguments()
		{
			const string expectedText = "some text";
			const int expectedValue = 15;

			_ctx.Register<CtorHavingType>("test")
				.BindConstructorArg<string>().ToValue(expectedText)
				.BindConstructorArg<int>().ToValue(expectedValue);

			var actual = _ctx.GetObject<CtorHavingType>("test");

			Assert.That(actual.Text, Is.EqualTo(expectedText));
			Assert.That(actual.Value, Is.EqualTo(expectedValue));
		}

		[Test]
		public void Inject_references_by_ctor_using_indexed_constructor_arguments()
		{
			_ctx.Register<CtorHavingType>("test")
				.BindConstructorArg<NestingType>(0).ToReference("nesting");

			_ctx.Register<NestingType>("nesting");

			NestingType actual = _ctx.GetObject<CtorHavingType>("test").Nesting;
			NestingType expected = _ctx.GetObject<NestingType>("nesting");

			Assert.That(actual, Is.SameAs(expected));
		}

		[Test]
		public void Inject_references_by_ctor_using_generic_constructor_arguments()
		{
			_ctx.Register<CtorHavingType>("test")
				.BindConstructorArg<NestingType>().ToReference("nesting");

			_ctx.Register<NestingType>("nesting");

			NestingType actual = _ctx.GetObject<CtorHavingType>("test").Nesting;
			NestingType expected = _ctx.GetObject<NestingType>("nesting");

			Assert.That(actual, Is.SameAs(expected));
		}

		[Test]
		public void Inject_default_references_by_ctor()
		{
			_ctx.Register<CtorHavingType>()
				.BindConstructorArg<NestingType>().ToDefaultReference();

			_ctx.Register<NestingType>();

			NestingType actual = _ctx.GetObject<CtorHavingType>().Nesting;
			NestingType expected = _ctx.GetObject<NestingType>();

			Assert.That(actual, Is.SameAs(expected));
		}

		[Test]
		public void Inject_typed_references_by_ctor()
		{
			var nestedRef = _ctx.Register<NestingType>("nested").GetReference();

			_ctx.Register<CtorHavingType>()
				.BindConstructorArg<NestingType>().ToReference(nestedRef);


			NestingType actual = _ctx.GetObject<CtorHavingType>().Nesting;
			NestingType expected = _ctx.GetObject<NestingType>("nested");

			Assert.That(actual, Is.SameAs(expected));
		}

		[Test]
		public void Inject_typed_references_for_derived_classes_by_ctor()
		{
			var nestedRef = _ctx.Register<DerivedFromNestingType>("nested").GetReference();

			_ctx.Register<CtorHavingType>()
				.BindConstructorArg<NestingType>().ToReference(nestedRef);

			var actual = _ctx.GetObject<CtorHavingType>();

			Assert.That(actual.Nesting, Is.TypeOf<DerivedFromNestingType>());
		}

		[Test]
		public void Inject_default_references_for_derived_classes_by_ctor()
		{
			 _ctx.Register<DerivedFromNestingType>();

			_ctx.Register<CtorHavingType>()
				.BindConstructorArg<NestingType>().ToDefaultReferenceOfType<DerivedFromNestingType>();

			var actual = _ctx.GetObject<CtorHavingType>();

			Assert.That(actual.Nesting, Is.TypeOf<DerivedFromNestingType>());
		}

		[Test]
		public void Inject_inner_definitions_by_ctor_using_generic_constructor_arguments()
		{
			_ctx.Register<CtorHavingType>("test")
				.BindConstructorArg<NestingType>().ToInlineDefinition<NestingType>(
					def => def.BindProperty(n => n.Simple).ToReference("simple"));

			_ctx.Register<SimpleType>("simple");

			var actual = _ctx.GetObject<CtorHavingType>("test");

			Assert.That(actual.Nesting, Is.Not.Null);
			Assert.That(actual.Nesting.Simple, Is.SameAs(_ctx.GetObject<SimpleType>("simple")));
		}

		[Test]
		public void Inject_inner_definitions_by_ctor_using_indexed_constructor_arguments()
		{
			_ctx.Register<CtorHavingType>("test")
				.BindConstructorArg<NestingType>(0).ToInlineDefinition<NestingType>(
					def => def.BindProperty(n => n.Simple).ToReference("simple"));

			_ctx.Register<SimpleType>("simple");

			var actual = _ctx.GetObject<CtorHavingType>("test");

			Assert.That(actual.Nesting, Is.Not.Null);
			Assert.That(actual.Nesting.Simple, Is.SameAs(_ctx.GetObject<SimpleType>("simple")));
		}
	}
}
