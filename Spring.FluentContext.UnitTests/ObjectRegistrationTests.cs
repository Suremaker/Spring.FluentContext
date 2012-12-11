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
	public class ObjectRegistrationTests
	{
		private FluentApplicationContext _ctx;

		[SetUp]
		public void SetUp()
		{
			_ctx = new FluentApplicationContext();
		}

		[Test]
		public void Register_named_object()
		{
			_ctx.RegisterNamed<SimpleType>("test");

			Assert.That(_ctx.GetObject<SimpleType>("test"), Is.Not.Null);
		}

		[Test]
		public void Register_default_object()
		{
			_ctx.RegisterDefault<SimpleType>();
			Assert.That(_ctx.GetObject<SimpleType>(), Is.Not.Null);
		}

		[Test]
		public void Register_unique_object()
		{
			var reference = _ctx.RegisterUniquelyNamed<SimpleType>().GetReference();
			Assert.That(_ctx.GetObject(reference), Is.Not.Null);
		}

		[Test]
		public void Multiple_register_unique_object_generates_unique_objects()
		{
			var simple1Ref = _ctx.RegisterUniquelyNamed<SimpleType>().GetReference();
			var simple2Ref = _ctx.RegisterUniquelyNamed<SimpleType>().GetReference();

			Assert.That(simple1Ref, Is.Not.EqualTo(simple2Ref));
			Assert.That(_ctx.GetObject(simple1Ref), Is.Not.SameAs(_ctx.GetObject(simple2Ref)));
		}

		[Test]
		public void Register_named_object_and_get_its_ref()
		{
			var reference = _ctx.RegisterNamed<SimpleType>("test").GetReference();
			Assert.That(reference.Id, Is.EqualTo("test"));
		}

		[Test]
		public void Register_default_object_and_get_its_ref()
		{
			var reference = _ctx.RegisterDefault<SimpleType>().GetReference();

			var actual = _ctx.GetObject<SimpleType>(reference.Id);
			var expected = _ctx.GetObject<SimpleType>();
			Assert.That(actual, Is.SameAs(expected));
		}

		[Test]
		public void Register_default_using_derived_type()
		{
			_ctx.RegisterDefault<Calculator>();
			_ctx.RegisterDefaultAlias<ICalculator>().ToRegisteredDefault<Calculator>();

			Assert.That(_ctx.GetObject<ICalculator>(), Is.TypeOf<Calculator>());
		}

		[Test]
		public void Register_named_using_derived_type()
		{
			_ctx.RegisterNamed<Calculator>("impl");
			_ctx.RegisterNamedAlias<ICalculator>("test").ToRegistered<Calculator>("impl");
			
			Assert.That(_ctx.GetObject<ICalculator>("test"), Is.TypeOf<Calculator>());
		}

		[Test]
		public void Register_uniquely_named_using_derived_type()
		{
			var implRef = _ctx.RegisterUniquelyNamed<Calculator>().GetReference();
			ObjectRef<ICalculator> reference = _ctx.RegisterUniquelyNamedAlias<ICalculator>().ToRegistered(implRef)
				.GetReference();
			
			Assert.That(_ctx.GetObject(reference), Is.TypeOf<Calculator>());
		}

		[Test]
		public void Register_named_singleton()
		{
			SimpleType type = new SimpleType();
			_ctx.RegisterNamedSingleton("test", type);

			Assert.That(_ctx.GetObject<SimpleType>("test"), Is.SameAs(type));
		}

		[Test]
		public void Register_default_singleton()
		{
			SimpleType type = new SimpleType();
			_ctx.RegisterDefaultSingleton(type);
			
			Assert.That(_ctx.GetObject<SimpleType>(), Is.SameAs(type));
		}

		[Test]
		public void Register_uniquely_named_singleton()
		{
			SimpleType type = new SimpleType();
			var reference = _ctx.RegisterUniquelyNamedSingleton(type);
			
			Assert.That(_ctx.GetObject(reference), Is.SameAs(type));
		}
	}
}
