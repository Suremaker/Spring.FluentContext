using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		public void Inject_dependencies_by_ctor_using_indexed_constructor_arguments()
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
		public void Inject_dependencies_by_ctor_using_indexed_constructor_arguments_and_proper_ctor()
		{
			const string expectedText = "some text";

			_ctx.Register<CtorHavingType>("test")
				.BindConstructorArg<string>(0).ToValue(expectedText);

			var actual = _ctx.GetObject<CtorHavingType>("test");

			Assert.That(actual.Text, Is.EqualTo(expectedText));
			Assert.That(actual.Value, Is.EqualTo(0));
		}

		[Test]
		public void Inject_dependencies_by_generic_constructor_arguments_and_values()
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
	}
}
