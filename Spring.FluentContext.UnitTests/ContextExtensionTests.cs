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
using Spring.Context.Support;
using Spring.FluentContext.UnitTests.TestTypes;
using Spring.Objects.Factory;

namespace Spring.FluentContext.UnitTests
{
	[TestFixture]
	public class ContextExtensionTests
	{
		[Test]
		public void Get_named_object()
		{			
			var ctx = new StaticApplicationContext();
			ctx.ObjectFactory.RegisterSingleton("test", new SimpleType());

			Assert.That(ctx.GetObject<SimpleType>("test"), Is.Not.Null);
		}

		[Test]
		public void Throw_on_get_object_by_wrong_type()
		{
			var ctx = new StaticApplicationContext();
			ctx.ObjectFactory.RegisterSingleton("test", new SimpleType());

			Assert.Throws<ObjectNotOfRequiredTypeException>(() => ctx.GetObject<string>("test"));
		}
	}
}
