using System;
using NUnit.Framework;
using Spring.FluentContext.UnitTests.TestTypes;
using Spring.FluentContext.Utils;

namespace Spring.FluentContext.UnitTests
{
	[TestFixture]
	public class ObjectLookupMethodInjectionTests
	{
		private FluentApplicationContext _ctx;

		[SetUp]
		public void SetUp()
		{
			_ctx = new FluentApplicationContext();
			CountingType.ClearCounter();
		}

		[Test]
		public void Bind_lookup_method_to_prototype()
		{
			_ctx.RegisterNamed<TypeWithFactoryMethod>("test")
				.BindLookupMethod(t => t.CreateType()).ToRegistered("counting");

			_ctx.RegisterNamed<CountingType>("counting").AsPrototype();

			var actual = _ctx.GetObject<TypeWithFactoryMethod>("test");

			var object1 = actual.CreateType();
			var object2 = actual.CreateType();

			Assert.That(object1.CurrentCount, Is.EqualTo(1));
			Assert.That(object2.CurrentCount, Is.EqualTo(2));
		}

		[Test]
		public void Bind_lookup_method_to_singleton()
		{
			_ctx.RegisterNamed<TypeWithFactoryMethod>("test")
				.BindLookupMethod(t => t.CreateType()).ToRegistered("counting");

			_ctx.RegisterNamed<CountingType>("counting").AsSingleton();

			var actual = _ctx.GetObject<TypeWithFactoryMethod>("test");

			var object1 = actual.CreateType();
			var object2 = actual.CreateType();

			Assert.That(object1.CurrentCount, Is.EqualTo(1));
			Assert.That(object2.CurrentCount, Is.EqualTo(1));
		}

		[Test]
		public void Bind_lookup_method_to_default_reference()
		{
			_ctx.RegisterDefault<CountingType>().AsSingleton();
			_ctx.RegisterDefault<TypeWithFactoryMethod>()
				.BindLookupMethod(t => t.CreateType()).ToRegisteredDefault();

			Assert.That(_ctx.GetObject<TypeWithFactoryMethod>().CreateType().CurrentCount, Is.EqualTo(1));
		}

		[Test]
		public void Bind_lookup_method_to_typed_reference()
		{
			var countingRef = _ctx.RegisterNamed<CountingType>("counting").AsSingleton().GetReference();

			_ctx.RegisterDefault<TypeWithFactoryMethod>()
				.BindLookupMethod(t => t.CreateType()).ToRegistered(countingRef);

			Assert.That(_ctx.GetObject<TypeWithFactoryMethod>().CreateType().CurrentCount, Is.EqualTo(1));
		}

		[Test]
		public void Bind_lookup_method_to_typed_reference_of_derived_class()
		{
			var countingRef = _ctx.RegisterNamed<DerivedFromCountingType>("counting").AsSingleton().GetReference();

			_ctx.RegisterDefault<TypeWithFactoryMethod>()
				.BindLookupMethod(t => t.CreateType()).ToRegistered(countingRef);

			Assert.That(_ctx.GetObject<TypeWithFactoryMethod>().CreateType(), Is.TypeOf<DerivedFromCountingType>());
		}

		[Test]
		public void Bind_lookup_method_to_default_reference_of_derived_class()
		{
			_ctx.RegisterDefault<DerivedFromCountingType>().AsSingleton();

			_ctx.RegisterDefault<TypeWithFactoryMethod>()
				.BindLookupMethod(t => t.CreateType()).ToRegisteredDefaultOf<DerivedFromCountingType>();

			Assert.That(_ctx.GetObject<TypeWithFactoryMethod>().CreateType(), Is.TypeOf<DerivedFromCountingType>());
		}

		[Test]
		public void Bind_lookup_method_to_generic_method_throws_exception()
		{
			_ctx.RegisterDefault<DerivedFromCountingType>().AsSingleton();
			_ctx.RegisterDefault<CountingType>().AsSingleton();

			var ex = Assert.Throws<InvalidOperationException>(
				() => _ctx.RegisterDefault<GenericFactoryMethod>()
					.BindLookupMethod<CountingType>(f => f.CreateType<CountingType>()).ToRegisteredDefault());
			Assert.That(ex.Message, Is.EqualTo(
				string.Format("Lookup method binding for CreateType<Spring.FluentContext.UnitTests.TestTypes.CountingType>() in object named '{0}' is not supported, because target method is generic.", IdGenerator<GenericFactoryMethod>.GetDefaultId())));
		}

		[Test]
		public void Bind_protected_lookup_method_to_prototype()
		{
			_ctx.RegisterNamed<TypeWithProtectedFactoryMethod>("test")
				.BindLookupMethodNamed<CountingType>("CreateType").ToRegistered("counting");

			_ctx.RegisterNamed<CountingType>("counting").AsPrototype();

			var actual = _ctx.GetObject<TypeWithProtectedFactoryMethod>("test");

			Assert.That(actual.GetValue(), Is.EqualTo(1));
			Assert.That(actual.GetValue(), Is.EqualTo(2));
		}
	}
}
