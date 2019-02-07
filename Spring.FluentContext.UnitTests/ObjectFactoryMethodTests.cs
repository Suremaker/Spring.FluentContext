using System.Collections.Generic;
using NUnit.Framework;
using Spring.FluentContext.UnitTests.TestTypes;

namespace Spring.FluentContext.UnitTests
{
	[TestFixture]
	public class ObjectFactoryMethodTests
	{
		private FluentApplicationContext _ctx;

		[SetUp]
		public void SetUp()
		{
			_ctx = new FluentApplicationContext();
		}

		[Test]
		public void Use_static_factory_method_creates_object_by_static_method_call()
		{
			_ctx.RegisterDefault<ComplexType>()
				.UseStaticFactoryMethod(ComplexTypeFactory.CreateInstance);

			Assert.That(_ctx.GetDefaultObject<ComplexType>().Text, Is.EqualTo(ComplexTypeFactory.DefaultInstanceText));
		}

		[Test]
		public void Use_static_factory_method_creates_object_by_static_method_with_1_parameter_call()
		{
			const string expectedText1 = "some text";
			_ctx.RegisterDefault<ComplexType>()
				.UseStaticFactoryMethod((string text) => ComplexTypeFactory.CreateInstance(text))
				.BindMethodArg().ToValue(expectedText1);

			Assert.That(_ctx.GetDefaultObject<ComplexType>().Text, Is.EqualTo(expectedText1));
		}

		[Test]
		public void Use_static_factory_method_creates_object_by_static_method_with_2_parameters_call()
		{
			const string expectedText1 = "some text";
			const string expectedText2 = "some text2";
			_ctx.RegisterDefault<ComplexType>()
				.UseStaticFactoryMethod((string text, string text2) => ComplexTypeFactory.CreateInstance(text, text2))
				.BindMethodArg().ToValue(expectedText1)
				.BindMethodArg().ToValue(expectedText2);

			Assert.That(_ctx.GetDefaultObject<ComplexType>().Text, Is.EqualTo(expectedText1));
			Assert.That(_ctx.GetDefaultObject<ComplexType>().Simple.Text, Is.EqualTo(expectedText2));
		}

		[Test]
		public void Use_static_factory_method_creates_object_by_static_method_with_3_parameters_call()
		{
			const string expectedText1 = "some text";
			const string expectedText2 = "some text2";
			const int expectedValue = 66;
			_ctx.RegisterDefault<ComplexType>()
				.UseStaticFactoryMethod((string text, string text2, int value) => ComplexTypeFactory.CreateInstance(text, text2, value))
				.BindMethodArg().ToValue(expectedText1)
				.BindMethodArg().ToValue(expectedText2)
				.BindMethodArg().ToValue(expectedValue);

			Assert.That(_ctx.GetDefaultObject<ComplexType>().Text, Is.EqualTo(expectedText1));
			Assert.That(_ctx.GetDefaultObject<ComplexType>().Simple.Text, Is.EqualTo(expectedText2));
			Assert.That(_ctx.GetDefaultObject<ComplexType>().Simple.Value, Is.EqualTo(expectedValue));
		}

		[Test]
		public void Use_factory_method_creates_object_by_method_call_on_registered_default_object()
		{
			const string expectedValue = "some text";
			_ctx.RegisterDefault<ComplexTypeFactory>()
				.BindProperty(f => f.InstanceText).ToValue(expectedValue);

			_ctx.RegisterDefault<ComplexType>()
				.UseFactoryMethod<ComplexTypeFactory>(f => f.Create()).OfRegisteredDefault();

			Assert.That(_ctx.GetDefaultObject<ComplexType>().Text, Is.EqualTo(expectedValue));
		}

		[Test]
		public void Use_factory_method_creates_object_by_method_call_on_registered_named_object()
		{
			const string expectedValue = "some text";
			_ctx.RegisterNamed<ComplexTypeFactory>("factory")
				.BindProperty(f => f.InstanceText).ToValue(expectedValue);

			_ctx.RegisterDefault<ComplexType>()
				.UseFactoryMethod<ComplexTypeFactory>(f => f.Create()).OfRegistered("factory");

			Assert.That(_ctx.GetDefaultObject<ComplexType>().Text, Is.EqualTo(expectedValue));
		}

		[Test]
		public void Use_factory_method_creates_object_by_method_call_on_referenced_registered_object()
		{
			const string expectedValue = "some text";
			var reference = _ctx.RegisterUniquelyNamed<ComplexTypeFactory>()
				.BindProperty(f => f.InstanceText).ToValue(expectedValue)
				.GetReference();

			_ctx.RegisterDefault<ComplexType>()
				.UseFactoryMethod<ComplexTypeFactory>(f => f.Create()).OfRegistered(reference);

			Assert.That(_ctx.GetDefaultObject<ComplexType>().Text, Is.EqualTo(expectedValue));
		}

		[Test]
		public void Use_factory_method_creates_object_by_method_with_1_parameter_call_on_referenced_registered_object()
		{
			const string expectedValue = "some text";
			var reference = _ctx.RegisterUniquelyNamed<ComplexTypeFactory>()
				.GetReference();

			_ctx.RegisterDefault<ComplexType>()
				.UseFactoryMethod((ComplexTypeFactory factory, string text) => factory.Create(text)).OfRegistered(reference)
				.BindMethodArg().ToValue(expectedValue);

			Assert.That(_ctx.GetDefaultObject<ComplexType>().Text, Is.EqualTo(expectedValue));
		}

		[Test]
		public void Use_factory_method_creates_object_by_method_with_2_parameters_call_on_referenced_registered_object()
		{
			const string expectedValue = "some text";
			const string expectedValue2 = "some text";
			var reference = _ctx.RegisterUniquelyNamed<ComplexTypeFactory>()
				.GetReference();

			_ctx.RegisterDefault<ComplexType>()
				.UseFactoryMethod((ComplexTypeFactory factory, string text, string text2) => factory.Create(text, text2)).OfRegistered(reference)
				.BindMethodArg().ToValue(expectedValue)
				.BindMethodArg().ToValue(expectedValue2);

			Assert.That(_ctx.GetDefaultObject<ComplexType>().Text, Is.EqualTo(expectedValue));
			Assert.That(_ctx.GetDefaultObject<ComplexType>().Simple.Text, Is.EqualTo(expectedValue2));
		}

		[Test]
		public void Use_factory_method_creates_object_by_method_with_3_parameters_call_on_referenced_registered_object()
		{
			const string expectedValue = "some text";
			const string expectedValue2 = "some text";
			const int expectedValue3 = 55;
			var reference = _ctx.RegisterUniquelyNamed<ComplexTypeFactory>()
				.GetReference();

			_ctx.RegisterDefault<ComplexType>()
				.UseFactoryMethod((ComplexTypeFactory factory, string text, string text2, int value) => factory.Create(text, text2, value)).OfRegistered(reference)
				.BindMethodArg().ToValue(expectedValue)
				.BindMethodArg().ToValue(expectedValue2)
				.BindMethodArg().ToValue(expectedValue3);

			Assert.That(_ctx.GetDefaultObject<ComplexType>().Text, Is.EqualTo(expectedValue));
			Assert.That(_ctx.GetDefaultObject<ComplexType>().Simple.Text, Is.EqualTo(expectedValue2));
			Assert.That(_ctx.GetDefaultObject<ComplexType>().Simple.Value, Is.EqualTo(expectedValue3));
		}

		[Test]
		public void Use_generic_factory_method_creates_object_by_generic_method_call_on_referenced_registered_object()
		{
			var reference = _ctx.RegisterUniquelyNamed<GenericTypeFactory>().GetReference();

			_ctx.RegisterDefault<ComplexType>()
				.UseFactoryMethod<GenericTypeFactory>(f => f.Create<ComplexType>()).OfRegistered(reference);

			Assert.That(_ctx.GetDefaultObject<ComplexType>(), Is.Not.Null);
		}

		[Test]
		public void Use_generic_static_factory_method_creates_object_by_generic_static_method_call()
		{
			_ctx.RegisterDefault<ComplexType>()
				.UseStaticFactoryMethod(GenericTypeFactory.CreateInstance<ComplexType>);

			Assert.That(_ctx.GetDefaultObject<ComplexType>(), Is.Not.Null);
		}

		[Test]
		public void Use_generic_static_factory_method_creates_generic_object_by_generic_static_method_call()
		{
			_ctx.RegisterDefault<List<int>>()
				.UseStaticFactoryMethod(GenericTypeFactory.CreateInstance<List<int>>);

			Assert.That(_ctx.GetDefaultObject<List<int>>(), Is.Not.Null);
		}
	}
}

