using NUnit.Framework;
using Spring.FluentContext.UnitTests.TestTypes;
using Spring.Objects.Factory.Config;

namespace Spring.FluentContext.UnitTests
{
	[TestFixture]
	public class ObjectAutowiringTests
	{
		private FluentApplicationContext _ctx;
		
		[SetUp]
		public void SetUp()
		{
			_ctx = new FluentApplicationContext();
		}

		[Test]
		public void Autowire_with_automatic_mode()
		{
			_ctx.RegisterNamed<NestingType>("nesting").Autowire();
			_ctx.RegisterNamed<SimpleType>("simple");
			_ctx.RegisterNamed<OtherType>("other");
			
			var actual = _ctx.GetObject<NestingType>("nesting");
			var expectedSimple = _ctx.GetObject<SimpleType>("simple");
			var expectedOther = _ctx.GetObject<OtherType>("other");

			Assert.That(actual.Simple, Is.SameAs(expectedSimple));
			Assert.That(actual.Other, Is.SameAs(expectedOther));
		}

		[Test]
		public void Autowire_is_disabled_by_default()
		{
			_ctx.RegisterNamed<NestingType>("nesting");
			_ctx.RegisterNamed<SimpleType>("simple");
			_ctx.RegisterNamed<OtherType>("other");

			Assert.That(_ctx.GetObject<NestingType>("nesting").Simple, Is.Null);
			Assert.That(_ctx.GetObject<NestingType>("nesting").Other, Is.Null);
		}

		[Test]
		public void Autowire_by_name()
		{
			_ctx.RegisterNamed<NestingType>("Nesting").Autowire(AutoWiringMode.ByName);
			_ctx.RegisterNamed<SimpleType>("Simple");
			_ctx.RegisterNamed<OtherType>("OtherType");

			var actual = _ctx.GetObject<NestingType>("Nesting");
			var expectedSimple = _ctx.GetObject<SimpleType>("Simple");
			
			Assert.That(actual.Simple, Is.SameAs(expectedSimple));
			Assert.That(actual.Other, Is.Null);
		}

		[Test]
		public void Autowire_by_type()
		{
			_ctx.RegisterNamed<NestingType>("nesting").Autowire(AutoWiringMode.ByType);
			_ctx.RegisterNamed<SimpleType>("notAsSimple");
			_ctx.RegisterNamed<OtherType>("quiteOther");
			
			var actual = _ctx.GetObject<NestingType>("nesting");
			var expectedSimple = _ctx.GetObject<SimpleType>("notAsSimple");
			var expectedOther = _ctx.GetObject<OtherType>("quiteOther");
			
			Assert.That(actual.Simple, Is.SameAs(expectedSimple));
			Assert.That(actual.Other, Is.SameAs(expectedOther));
		}

		[Test]
		public void Autowire_by_constructor()
		{
			_ctx.RegisterNamed<CtorHavingType>("test").Autowire(AutoWiringMode.Constructor);
			_ctx.RegisterNamed<NestingType>("nesting");
			
			var expected = _ctx.GetObject<NestingType>("nesting");
			var actual = _ctx.GetObject<CtorHavingType>("test");
			
			Assert.That(actual.Nesting, Is.SameAs(expected));
		}
	}
}

