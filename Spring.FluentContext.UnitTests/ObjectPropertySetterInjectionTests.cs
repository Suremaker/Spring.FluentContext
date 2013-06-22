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

using System.Linq;
using NUnit.Framework;
using Spring.FluentContext.Definitions;
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
				.BindProperty(n => n.Simple).ToRegisteredDefaultOf<DerivedFromSimpleType>();

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
				.BindProperty(n => n.Simple)
				.ToInlineDefinition<SimpleType>(
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

		[Test]
		public void Bind_property_to_references_using_generic_binding()
		{
			_ctx.RegisterDefault<NestingType>()
				.BindProperty(t => t.Other).To(Def.Reference<OtherType>())
				.BindProperty(t => t.Simple).To(Def.Reference<SimpleType>("simple"));

			_ctx.RegisterNamed<SimpleType>("simple");
			_ctx.RegisterDefault<OtherType>();

			var actual = _ctx.GetObject<NestingType>();
			Assert.That(actual.Simple, Is.SameAs(_ctx.GetObject<SimpleType>("simple")));
			Assert.That(actual.Other, Is.SameAs(_ctx.GetObject<OtherType>()));
		}

		[Test]
		public void Bind_property_to_value_using_generic_binding()
		{
			const string expected = "test";
			_ctx.RegisterDefault<SimpleType>()
				.BindProperty(s => s.Text).To(Def.Value(expected));

			var actual = _ctx.GetObject<SimpleType>();
			Assert.That(actual.Text, Is.EqualTo(expected));
		}

		[Test]
		public void Bind_property_to_ref_using_generic_binding()
		{
			var reference = _ctx.RegisterDefault<SimpleType>().GetReference();
			_ctx.RegisterDefault<NestingType>()
				.BindProperty(n => n.Simple).To(reference);

			Assert.That(_ctx.GetObject<NestingType>().Simple, Is.SameAs(_ctx.GetObject<SimpleType>()));
		}

		[Test]
		public void Bind_property_to_array_using_generic_binding()
		{
			_ctx.RegisterDefault<SimpleType>()
				.BindProperty(s => s.Text).ToValue("1");

			_ctx.RegisterNamed<SimpleType>("test")
				.BindProperty(s => s.Text).ToValue("2");

			_ctx.RegisterDefault<CollectionHolder>()
				.BindProperty(h => h.Array).To(Def.Array(
					Def.Reference<SimpleType>(),
					Def.Reference<SimpleType>("test"),
					Def.Value(new SimpleType { Text = "3" })));

			Assert.That(_ctx.GetObject<CollectionHolder>().Array.Select(v => v.Text), Is.EquivalentTo(new[] { "1", "2", "3" }));
		}

		[Test]
		public void Bind_property_to_array_using_extension_binding()
		{
			_ctx.RegisterDefault<SimpleType>()
				.BindProperty(s => s.Text).ToValue("1");

			_ctx.RegisterNamed<SimpleType>("test")
				.BindProperty(s => s.Text).ToValue("2");

			_ctx.RegisterDefault<CollectionHolder>()
				.BindProperty(h => h.Array).ToArray(
					Def.Reference<SimpleType>(),
					Def.Reference<SimpleType>("test"),
					Def.Value(new SimpleType { Text = "3" }));

			Assert.That(_ctx.GetObject<CollectionHolder>().Array.Select(v => v.Text), Is.EquivalentTo(new[] { "1", "2", "3" }));
		}

		[Test]
		public void Bind_property_to_list_using_generic_binding()
		{
			_ctx.RegisterDefault<OtherType>()
				.BindProperty(s => s.Text).ToValue("1");

			_ctx.RegisterNamed<OtherType>("test")
				.BindProperty(s => s.Text).ToValue("2");

			_ctx.RegisterDefault<CollectionHolder>()
				.BindProperty(h => h.List).To(Def.List(
					Def.Reference<OtherType>(),
					Def.Reference<OtherType>("test"),
					Def.Value(new OtherType { Text = "3" })));

			Assert.That(_ctx.GetObject<CollectionHolder>().List.Select(v => v.Text), Is.EquivalentTo(new[] { "1", "2", "3" }));
		}

		[Test]
		public void Bind_property_to_collection_using_generic_binding()
		{
			_ctx.RegisterDefault<DerivedFromSimpleType>()
				.BindProperty(s => s.Text).ToValue("1");

			_ctx.RegisterNamed<DerivedFromSimpleType>("test")
				.BindProperty(s => s.Text).ToValue("2");

			_ctx.RegisterDefault<CollectionHolder>()
				.BindProperty(h => h.Collection).To(Def.Array(
					Def.Reference<DerivedFromSimpleType>(),
					Def.Reference<DerivedFromSimpleType>("test"),
					Def.Value(new DerivedFromSimpleType { Text = "3" })));

			Assert.That(_ctx.GetObject<CollectionHolder>().Collection.Select(v => v.Text), Is.EquivalentTo(new[] { "1", "2", "3" }));
		}

		[Test]
		public void Bind_property_to_dictionary_using_generic_binding()
		{
			_ctx.RegisterDefault<DerivedFromSimpleType>()
				.BindProperty(s => s.Text).ToValue("1");

			_ctx.RegisterDefault<SimpleType>()
				.BindProperty(s => s.Text).ToValue("2");

			_ctx.RegisterDefault<CollectionHolder>()
				.BindProperty(h => h.Dictionary).To(Def.Dictionary<int, SimpleType>(dict =>
				{
					dict[Def.Value(1)] = Def.Reference<DerivedFromSimpleType>();
					dict[Def.Value(2)] = Def.Reference<SimpleType>();
					dict[Def.Value(3)] = Def.Value(new SimpleType { Text = "3" });
				}));

			Assert.That(_ctx.GetObject<CollectionHolder>().Dictionary.Keys, Is.EquivalentTo(new[] { 1, 2, 3 }));
			Assert.That(_ctx.GetObject<CollectionHolder>().Dictionary.Values.Select(v => v.Text), Is.EquivalentTo(new[] { "1", "2", "3" }));
		}

		[Test]
		public void Bind_property_to_dictionary_where_one_value_is_overwritten()
		{
			_ctx.RegisterDefault<CollectionHolder>()
				.BindProperty(h => h.Dictionary).ToDictionary(dict =>
				{
					dict[Def.Value(1)] = Def.Reference<DerivedFromSimpleType>();
					dict[Def.Value(1)] = Def.Value(new SimpleType { Text = "abc" });
				});

			Assert.That(_ctx.GetObject<CollectionHolder>().Dictionary.Keys, Is.EquivalentTo(new[] { 1 }));
			Assert.That(_ctx.GetObject<CollectionHolder>().Dictionary.Values.Select(v => v.Text), Is.EquivalentTo(new[] { "abc" }));
		}

		[Test]
		public void Bind_property_to_dictionary_using_extension_binding()
		{
			_ctx.RegisterDefault<DerivedFromSimpleType>()
				.BindProperty(s => s.Text).ToValue("1");

			_ctx.RegisterDefault<SimpleType>()
				.BindProperty(s => s.Text).ToValue("2");

			_ctx.RegisterDefault<CollectionHolder>()
				.BindProperty(h => h.Dictionary).ToDictionary(dict =>
				{
					dict[Def.Value(1)] = Def.Reference<DerivedFromSimpleType>();
					dict[Def.Value(2)] = Def.Reference<SimpleType>();
					dict[Def.Value(3)] = Def.Value(new SimpleType { Text = "3" });
				});

			Assert.That(_ctx.GetObject<CollectionHolder>().Dictionary.Keys, Is.EquivalentTo(new[] { 1, 2, 3 }));
			Assert.That(_ctx.GetObject<CollectionHolder>().Dictionary.Values.Select(v => v.Text), Is.EquivalentTo(new[] { "1", "2", "3" }));
		}

		[Test]
		public void Bind_property_to_collection_using_extension_binding()
		{
			_ctx.RegisterDefault<OtherType>().BindProperty(t => t.Text).ToValue("2");
			_ctx.RegisterDefault<DerivedFromSimpleType>().BindProperty(t => t.Text).ToValue("2");
			_ctx.RegisterDefault<SimpleType>().BindProperty(t => t.Text).ToValue("2");

			_ctx.RegisterDefault<CollectionHolder>()
				.BindProperty(h => h.Collection).ToList(
					Def.Value(new DerivedFromSimpleType { Text = "1" }),
					Def.Reference<DerivedFromSimpleType>())
				.BindProperty(h => h.List).ToList(
					Def.Value(new OtherType { Text = "1" }),
					Def.Reference<OtherType>())
				.BindProperty(h => h.Array).ToArray(
					Def.Value(new SimpleType { Text = "1" }),
					Def.Reference<SimpleType>());

			Assert.That(_ctx.GetObject<CollectionHolder>().Collection.Select(v => v.Text), Is.EquivalentTo(new[] { "1", "2" }));
			Assert.That(_ctx.GetObject<CollectionHolder>().Array.Select(v => v.Text), Is.EquivalentTo(new[] { "1", "2" }));
			Assert.That(_ctx.GetObject<CollectionHolder>().List.Select(v => v.Text), Is.EquivalentTo(new[] { "1", "2" }));
		}

		[Test]
		public void Bind_property_to_inline_definition_using_generic_binding()
		{
			const string expectedText = "some text";
			const int expectedValue = 32;

			_ctx.RegisterNamed<NestingType>("nesting")
				.BindProperty(n => n.Simple).To(
					Def.Object<DerivedFromSimpleType>(def => def
						.BindProperty(s => s.Text).ToValue(expectedText)
						.BindProperty(s => s.Value).ToValue(expectedValue)));

			var actual = _ctx.GetObject<NestingType>("nesting");
			Assert.That(actual.Simple, Is.InstanceOf<DerivedFromSimpleType>());

			var simple = (DerivedFromSimpleType)actual.Simple;
			Assert.That(simple.Text, Is.EqualTo(expectedText));
			Assert.That(simple.Value, Is.EqualTo(expectedValue));
		}

		[Test]
		public void Bind_property_to_inline_default_definition_using_generic_binding()
		{
			_ctx.RegisterDefault<NestingType>()
				.BindProperty(n => n.Simple).To(Def.Object<DerivedFromSimpleType>());

			Assert.That(_ctx.GetObject<NestingType>().Simple, Is.InstanceOf<DerivedFromSimpleType>());
		}

		[Test]
		public void Bind_property_to_property_value_of_other_object()
		{
			var nestingRef = _ctx.RegisterUniquelyNamed<NestingType>()
				.BindProperty(n => n.Simple).To(Def.Object<SimpleType>()).GetReference();

			_ctx.RegisterDefault<NestingType>()
				.BindProperty(n => n.Simple).To(Def.ObjectProperty(
					nestingRef,
					n => n.Simple));

			Assert.That(_ctx.GetObject<NestingType>().Simple, Is.SameAs(_ctx.GetObject(nestingRef).Simple));
		}
	}
}
