//
//  Author:
//    Wojciech Kotlarski wojciech.kotlarski@gmail.com
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
		public void Bind_property_to_inline_definition()
		{
			const string expectedText = "some text";

			_ctx.RegisterNamed<NestingType>("nesting")
				.BindProperty(n => n.Simple).ToInlineDefinition<SimpleType>(
					def => def.BindProperty(s => s.Text).ToValue(expectedText));

			var actual = _ctx.GetObject<NestingType>("nesting");
			Assert.That(actual.Simple.Text, Is.EqualTo(expectedText));
		}

		[Test]
		public void Bind_property_to_simple_inline_definition()
		{
			_ctx.RegisterNamed<NestingType>("nesting")
				.BindProperty(n => n.Simple).ToInlineDefinition<SimpleType>();

			var actual = _ctx.GetObject<NestingType>("nesting");
			Assert.That(actual.Simple, Is.Not.Null);
		}

		[Test]
		public void Inline_defined_object_are_always_prototypes()
		{
			_ctx.RegisterDefault<NestingType>()
				.AsPrototype()
				.BindProperty(n => n.Simple).ToInlineDefinition<SimpleType>();

			var nesting1 = _ctx.GetObject<NestingType>();
			var nesting2 = _ctx.GetObject<NestingType>();

			Assert.That(nesting1.Simple, Is.Not.SameAs(nesting2.Simple));
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
