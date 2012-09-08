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
