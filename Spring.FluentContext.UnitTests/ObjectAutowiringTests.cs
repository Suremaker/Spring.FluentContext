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

