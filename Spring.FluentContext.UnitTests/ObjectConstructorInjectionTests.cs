//
//  Author:
//    Wojciech Kotlarski
//
//  Copyright (c) 2012, Wojciech Kotlarski
//
//  All rights reserved.
//
//  Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.	 
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in
//       the documentation and/or other materials provided with the distribution.
//
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
//  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT 
//  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS 
//  FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR 
//  CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, 
//  EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, 
//  PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF 
//  LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
//  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
//  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
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

			_ctx.RegisterNamed<CtorHavingType>("test")
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

			_ctx.RegisterNamed<CtorHavingType>("test")
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

			_ctx.RegisterNamed<CtorHavingType>("test")
				.BindConstructorArg<string>().ToValue(expectedText)
				.BindConstructorArg<int>().ToValue(expectedValue);

			var actual = _ctx.GetObject<CtorHavingType>("test");

			Assert.That(actual.Text, Is.EqualTo(expectedText));
			Assert.That(actual.Value, Is.EqualTo(expectedValue));
		}

		[Test]
		public void Inject_references_by_ctor_using_indexed_constructor_arguments()
		{
			_ctx.RegisterNamed<CtorHavingType>("test")
				.BindConstructorArg<NestingType>(0).ToRegistered("nesting");

			_ctx.RegisterNamed<NestingType>("nesting");

			NestingType actual = _ctx.GetObject<CtorHavingType>("test").Nesting;
			NestingType expected = _ctx.GetObject<NestingType>("nesting");

			Assert.That(actual, Is.SameAs(expected));
		}

		[Test]
		public void Inject_references_by_ctor_using_generic_constructor_arguments()
		{
			_ctx.RegisterNamed<CtorHavingType>("test")
				.BindConstructorArg<NestingType>().ToRegistered("nesting");

			_ctx.RegisterNamed<NestingType>("nesting");

			NestingType actual = _ctx.GetObject<CtorHavingType>("test").Nesting;
			NestingType expected = _ctx.GetObject<NestingType>("nesting");

			Assert.That(actual, Is.SameAs(expected));
		}

		[Test]
		public void Inject_default_references_by_ctor()
		{
			_ctx.RegisterDefault<CtorHavingType>()
				.BindConstructorArg<NestingType>().ToRegisteredDefault();

			_ctx.RegisterDefault<NestingType>();

			NestingType actual = _ctx.GetObject<CtorHavingType>().Nesting;
			NestingType expected = _ctx.GetObject<NestingType>();

			Assert.That(actual, Is.SameAs(expected));
		}

		[Test]
		public void Inject_typed_references_by_ctor()
		{
			var nestedRef = _ctx.RegisterNamed<NestingType>("nested").GetReference();

			_ctx.RegisterDefault<CtorHavingType>()
				.BindConstructorArg<NestingType>().ToRegistered(nestedRef);


			NestingType actual = _ctx.GetObject<CtorHavingType>().Nesting;
			NestingType expected = _ctx.GetObject<NestingType>("nested");

			Assert.That(actual, Is.SameAs(expected));
		}

		[Test]
		public void Inject_typed_references_for_derived_classes_by_ctor()
		{
			var nestedRef = _ctx.RegisterNamed<DerivedFromNestingType>("nested").GetReference();

			_ctx.RegisterDefault<CtorHavingType>()
				.BindConstructorArg<NestingType>().ToRegistered(nestedRef);

			var actual = _ctx.GetObject<CtorHavingType>();

			Assert.That(actual.Nesting, Is.TypeOf<DerivedFromNestingType>());
		}

		[Test]
		public void Inject_default_references_for_derived_classes_by_ctor()
		{
			_ctx.RegisterDefault<DerivedFromNestingType>();

			_ctx.RegisterDefault<CtorHavingType>()
				.BindConstructorArg<NestingType>().ToRegisteredDefaultOfType<DerivedFromNestingType>();

			var actual = _ctx.GetObject<CtorHavingType>();

			Assert.That(actual.Nesting, Is.TypeOf<DerivedFromNestingType>());
		}

		[Test]
		public void Inject_inline_definitions_by_ctor_using_generic_constructor_arguments()
		{
			_ctx.RegisterNamed<CtorHavingType>("test")
				.BindConstructorArg<NestingType>().ToInlineDefinition<NestingType>(
					def => def.BindProperty(n => n.Simple).ToRegistered("simple"));

			_ctx.RegisterNamed<SimpleType>("simple");

			var actual = _ctx.GetObject<CtorHavingType>("test");

			Assert.That(actual.Nesting, Is.Not.Null);
			Assert.That(actual.Nesting.Simple, Is.SameAs(_ctx.GetObject<SimpleType>("simple")));
		}

		[Test]
		public void Inject_inline_definitions_by_ctor_using_indexed_constructor_arguments()
		{
			_ctx.RegisterNamed<CtorHavingType>("test")
				.BindConstructorArg<NestingType>(0).ToInlineDefinition<NestingType>(
					def => def.BindProperty(n => n.Simple).ToRegistered("simple"));

			_ctx.RegisterNamed<SimpleType>("simple");

			var actual = _ctx.GetObject<CtorHavingType>("test");

			Assert.That(actual.Nesting, Is.Not.Null);
			Assert.That(actual.Nesting.Simple, Is.SameAs(_ctx.GetObject<SimpleType>("simple")));
		}

		[Test]
		public void Inject_simple_inline_definitions_by_ctor_using_generic_constructor_arguments()
		{
			_ctx.RegisterNamed<CtorHavingType>("test")
				.BindConstructorArg<NestingType>().ToInlineDefinition<NestingType>();

			var actual = _ctx.GetObject<CtorHavingType>("test");

			Assert.That(actual.Nesting, Is.Not.Null);
		}

		[Test]
		public void Inject_simple_inline_definitions_by_ctor_using_indexed_constructor_arguments()
		{
			_ctx.RegisterNamed<CtorHavingType>("test")
				.BindConstructorArg<NestingType>(0).ToInlineDefinition<NestingType>();

			var actual = _ctx.GetObject<CtorHavingType>("test");

			Assert.That(actual.Nesting, Is.Not.Null);
		}

		[Test]
		public void Inline_defined_object_are_always_prototypes()
		{
			_ctx.RegisterDefault<CtorHavingType>()
				.AsPrototype()
				.BindConstructorArg<NestingType>().ToInlineDefinition<NestingType>();

			var nesting1 = _ctx.GetObject<CtorHavingType>();
			var nesting2 = _ctx.GetObject<CtorHavingType>();

			Assert.That(nesting1.Nesting, Is.Not.SameAs(nesting2.Nesting));
		}

		[Test]
		public void Bind_one_parameter_constructor()
		{
			string expectedText = "some text";
			_ctx.RegisterDefault<CtorHavingType>()
				.UseConstructor((string text) => new CtorHavingType(text))
				.BindConstructorArg().ToValue(expectedText);

			Assert.That(_ctx.GetObject<CtorHavingType>().Text, Is.EqualTo(expectedText));
		}

		[Test]
		public void Bind_two_parameter_constructor()
		{
			string expectedText = "some text";
			int expectedValue = 5;

			_ctx.RegisterDefault<CtorHavingType>()
				.UseConstructor((string text, int value) => new CtorHavingType(text, value))
					.BindConstructorArg().ToValue(expectedText)
					.BindConstructorArg().ToValue(expectedValue);
			
			Assert.That(_ctx.GetObject<CtorHavingType>().Text, Is.EqualTo(expectedText));
			Assert.That(_ctx.GetObject<CtorHavingType>().Value, Is.EqualTo(expectedValue));
		}

		[Test]
		public void Bind_three_parameter_constructor()
		{
			string expectedText = "some text";
			int expectedValue = 5;

			_ctx.RegisterDefault<NestingType>();

			_ctx.RegisterDefault<CtorHavingType>()
				.UseConstructor((NestingType nesting, string text, int value) => new CtorHavingType(nesting, text, value))
					.BindConstructorArg().ToRegisteredDefault()
					.BindConstructorArg().ToValue(expectedText)
					.BindConstructorArg().ToValue(expectedValue);
			
			Assert.That(_ctx.GetObject<CtorHavingType>().Text, Is.EqualTo(expectedText));
			Assert.That(_ctx.GetObject<CtorHavingType>().Value, Is.EqualTo(expectedValue));
			Assert.That(_ctx.GetObject<CtorHavingType>().Nesting, Is.SameAs(_ctx.GetObject<NestingType>()));
		}

		[Test]
		public void Bind_four_parameter_constructor()
		{
			string expectedText = "some text";
			int expectedValue = 5;
			double expectedOther = 5.5;

			_ctx.RegisterDefault<NestingType>();
			_ctx.RegisterDefault<CtorHavingType>()
				.UseConstructor((NestingType n, string t, int v, double o) => new CtorHavingType(n, t, v, o))
					.BindConstructorArg().ToRegisteredDefault()
					.BindConstructorArg().ToValue(expectedText)
					.BindConstructorArg().ToValue(expectedValue)
					.BindConstructorArg().ToValue(expectedOther);
			
			Assert.That(_ctx.GetObject<CtorHavingType>().Text, Is.EqualTo(expectedText));
			Assert.That(_ctx.GetObject<CtorHavingType>().Value, Is.EqualTo(expectedValue));
			Assert.That(_ctx.GetObject<CtorHavingType>().Nesting, Is.SameAs(_ctx.GetObject<NestingType>()));
			Assert.That(_ctx.GetObject<CtorHavingType>().OtherValue, Is.EqualTo(expectedOther));
		}
	}
}
