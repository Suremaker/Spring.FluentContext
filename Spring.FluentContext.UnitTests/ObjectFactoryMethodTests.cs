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

			Assert.That(_ctx.GetObject<ComplexType>().Text, Is.EqualTo(ComplexTypeFactory.DefaultInstanceText));
		}

		[Test]
		public void Use_factory_method_creates_object_by_method_call_on_registered_default_object()
		{
			string expectedValue = "some text";
			_ctx.RegisterDefault<ComplexTypeFactory>()
				.BindProperty(f => f.InstanceText).ToValue(expectedValue);

			_ctx.RegisterDefault<ComplexType>()
				.UseFactoryMethod<ComplexTypeFactory>(f => f.Create()).OfRegisteredDefault();

			Assert.That(_ctx.GetObject<ComplexType>().Text, Is.EqualTo(expectedValue));
		}

		[Test]
		public void Use_factory_method_creates_object_by_method_call_on_registered_named_object()
		{
			string expectedValue = "some text";
			_ctx.RegisterNamed<ComplexTypeFactory>("factory")
				.BindProperty(f => f.InstanceText).ToValue(expectedValue);

			_ctx.RegisterDefault<ComplexType>()
				.UseFactoryMethod<ComplexTypeFactory>(f => f.Create()).OfRegistered("factory");

			Assert.That(_ctx.GetObject<ComplexType>().Text, Is.EqualTo(expectedValue));
		}

		[Test]
		public void Use_factory_method_creates_object_by_method_call_on_referenced_registered_object()
		{
			string expectedValue = "some text";
			var reference = _ctx.RegisterUniquelyNamed<ComplexTypeFactory>()
				.BindProperty(f => f.InstanceText).ToValue(expectedValue)
				.GetReference();

			_ctx.RegisterDefault<ComplexType>()
				.UseFactoryMethod<ComplexTypeFactory>(f => f.Create()).OfRegistered(reference);

			Assert.That(_ctx.GetObject<ComplexType>().Text, Is.EqualTo(expectedValue));
		}

		[Test]
		public void Use_generic_factory_method_creates_object_by_generic_method_call_on_referenced_registered_object()
		{
			var reference = _ctx.RegisterUniquelyNamed<GenericTypeFactory>().GetReference();

			_ctx.RegisterDefault<ComplexType>()
				.UseFactoryMethod<GenericTypeFactory>(f => f.Create<ComplexType>()).OfRegistered(reference);

			Assert.That(_ctx.GetObject<ComplexType>(), Is.Not.Null);
		}

		[Test]
		public void Use_generic_static_factory_method_creates_object_by_generic_static_method_call()
		{
			_ctx.RegisterDefault<ComplexType>()
				.UseStaticFactoryMethod(GenericTypeFactory.CreateInstance<ComplexType>);

			Assert.That(_ctx.GetObject<ComplexType>(), Is.Not.Null);
		}

		[Test]
		public void Use_generic_static_factory_method_creates_generic_object_by_generic_static_method_call()
		{
			_ctx.RegisterDefault<List<int>>()
				.UseStaticFactoryMethod(GenericTypeFactory.CreateInstance<List<int>>);

			Assert.That(_ctx.GetObject<List<int>>(), Is.Not.Null);
		}
	}
}

