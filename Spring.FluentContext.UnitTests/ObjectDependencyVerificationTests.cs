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
using Spring.Objects.Factory;
using Spring.Objects.Factory.Support;

namespace Spring.FluentContext.UnitTests
{
	[TestFixture]
	public class ObjectDependencyVerificationTests
	{
		private FluentApplicationContext _ctx;
		
		[SetUp]
		public void SetUp()
		{
			_ctx = new FluentApplicationContext();
		}
		
		[Test]
		public void Check_all_dependencies()
		{
			_ctx.RegisterNamed<SimpleType>("simple");

			_ctx.RegisterNamed<ComplexType>("test")
				.BindProperty(c => c.Simple).ToRegistered("simple")
				.BindProperty(c => c.Text).ToValue("text")
				.CheckDependencies();

			Assert.DoesNotThrow(() => _ctx.GetObject<ComplexType>("test"));
		}

		[Test]
		public void Check_all_dependencies_throws_on_instantiation_if_missing_object_dependency()
		{
			_ctx.RegisterNamed<ComplexType>("test")
				.BindProperty(c => c.Text).ToValue("text")
				.CheckDependencies();
			
			Assert.Throws<UnsatisfiedDependencyException>(() => _ctx.GetObject<ComplexType>("test"));
		}

		[Test]
		public void Check_all_dependencies_throws_on_instantiation_if_missing_simple_dependency()
		{
			_ctx.RegisterNamed<SimpleType>("simple");
			
			_ctx.RegisterNamed<ComplexType>("test")
				.BindProperty(c => c.Simple).ToRegistered("simple")
				.CheckDependencies();
			
			Assert.Throws<UnsatisfiedDependencyException>(() => _ctx.GetObject<ComplexType>("test"));
		}

		[Test]
		public void Check_objects_dependencies()
		{
			_ctx.RegisterNamed<SimpleType>("simple");
			
			_ctx.RegisterNamed<ComplexType>("test")
				.BindProperty(c => c.Simple).ToRegistered("simple")
				.CheckDependencies(DependencyCheckingMode.Objects);
			
			Assert.DoesNotThrow(() => _ctx.GetObject<ComplexType>("test"));
		}
		
		[Test]
		public void Check_objects_dependencies_throws_on_instantiation_if_missing_object_dependency()
		{
			_ctx.RegisterNamed<ComplexType>("test")
				.CheckDependencies(DependencyCheckingMode.Objects);
			
			Assert.Throws<UnsatisfiedDependencyException>(() => _ctx.GetObject<ComplexType>("test"));
		}

		[Test]
		public void Check_simple_dependencies()
		{
			_ctx.RegisterNamed<ComplexType>("test")
				.BindProperty(c => c.Text).ToValue("text")
				.CheckDependencies(DependencyCheckingMode.Simple);
			
			Assert.DoesNotThrow(() => _ctx.GetObject<ComplexType>("test"));
		}
		
		[Test]
		public void Check_simple_dependencies_throws_on_instantiation_if_missing_simple_dependency()
		{
			_ctx.RegisterNamed<ComplexType>("test")
				.CheckDependencies(DependencyCheckingMode.Simple);
			
			Assert.Throws<UnsatisfiedDependencyException>(() => _ctx.GetObject<ComplexType>("test"));
		}
	}
}

