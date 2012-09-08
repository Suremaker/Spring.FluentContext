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
	public class FactoryRegistrationTests
	{
		private FluentApplicationContext _ctx;

		[SetUp]
		public void SetUp()
		{
			_ctx = new FluentApplicationContext();
			CountingType.ClearCounter();
			OtherCountingType.ClearCounter();
		}

		[Test]
		public void Register_proxy_factory()
		{
			_ctx.RegisterNamed<Calculator>("calcImpl");
			_ctx.RegisterNamedProxyFactory<ICalculator>("calc")
				.Targeting("calcImpl");

			Assert.That(_ctx.GetObject<ICalculator>("calc").Add(3, 5), Is.EqualTo(8));
		}

		[Test]
		public void Register_proxy_factory_for_typed_reference()
		{
			var reference = _ctx.RegisterDefault<Calculator>().GetReference();
			_ctx.RegisterNamedProxyFactory<ICalculator>("calc")
				.Targeting(reference);

			Assert.That(_ctx.GetObject<ICalculator>("calc").Add(3, 5), Is.EqualTo(8));
		}

		[Test]
		public void Register_proxy_factory_for_default_target()
		{
			_ctx.RegisterDefault<Calculator>();
			_ctx.RegisterNamedProxyFactory<ICalculator>("calc")
				.TargetingDefaultOfType<Calculator>();

			Assert.That(_ctx.GetObject<ICalculator>("calc").Add(3, 5), Is.EqualTo(8));
		}

		[Test]
		public void Register_proxy_factory_with_named_interceptor()
		{
			_ctx.RegisterDefault<Calculator>();
			_ctx.RegisterNamed<MultiplingInterceptor>("interceptor");
			_ctx.RegisterNamedProxyFactory<ICalculator>("calc").TargetingDefaultOfType<Calculator>()
				.AddInterceptor("interceptor");

			Assert.That(_ctx.GetObject<ICalculator>("calc").Add(3, 5), Is.EqualTo(80));
		}

		[Test]
		public void Register_proxy_factory_with_typed_interceptor()
		{
			_ctx.RegisterDefault<Calculator>();
			var interceptorRef = _ctx.RegisterDefault<MultiplingInterceptor>().GetReference();
			_ctx.RegisterNamedProxyFactory<ICalculator>("calc").TargetingDefaultOfType<Calculator>()
				.AddInterceptor(interceptorRef);

			Assert.That(_ctx.GetObject<ICalculator>("calc").Add(3, 5), Is.EqualTo(80));
		}

		[Test]
		public void Register_proxy_factory_with_default_interceptor()
		{
			_ctx.RegisterDefault<Calculator>();
			_ctx.RegisterDefault<MultiplingInterceptor>();
			_ctx.RegisterNamedProxyFactory<ICalculator>("calc").TargetingDefaultOfType<Calculator>()
				.AddInterceptorByDefaultReference<MultiplingInterceptor>();

			Assert.That(_ctx.GetObject<ICalculator>("calc").Add(3, 5), Is.EqualTo(80));
		}

		[Test]
		public void Register_default_proxy_factory()
		{
			_ctx.RegisterDefault<Calculator>();
			_ctx.RegisterDefault<MultiplingInterceptor>();
			_ctx.RegisterDefaultProxyFactory<ICalculator>()
				.TargetingDefaultOfType<Calculator>()
				.AddInterceptorByDefaultReference<MultiplingInterceptor>();

			Assert.That(_ctx.GetObject<ICalculator>().Add(3, 5), Is.EqualTo(80));
		}

		[Test]
		public void Register_unique_proxy_factory()
		{
			_ctx.RegisterDefault<Calculator>();
			_ctx.RegisterDefault<MultiplingInterceptor>();
			var proxyRef = _ctx.RegisterNamedProxyFactory<ICalculator>("factory")
				.TargetingDefaultOfType<Calculator>()
				.AddInterceptorByDefaultReference<MultiplingInterceptor>()
				.GetReference();

			Assert.That(_ctx.GetObject<ICalculator>(proxyRef).Add(3, 5), Is.EqualTo(80));
		}

		[Test]
		public void Register_proxy_factory_to_return_prototypes()
		{
			_ctx.RegisterDefault<Calculator>();
			_ctx.RegisterDefaultProxyFactory<ICalculator>()
				.TargetingDefaultOfType<Calculator>()
				.ReturningPrototypes();

			var proxy1 = _ctx.GetObject<ICalculator>();
			var proxy2 = _ctx.GetObject<ICalculator>();

			Assert.That(proxy1, Is.Not.SameAs(proxy2));
		}

		[Test]
		public void Proxy_factory_returns_singleton_by_default()
		{
			_ctx.RegisterDefault<Calculator>();
			_ctx.RegisterDefaultProxyFactory<ICalculator>()
				.TargetingDefaultOfType<Calculator>();
			
			var proxy1 = _ctx.GetObject<ICalculator>();
			var proxy2 = _ctx.GetObject<ICalculator>();
			
			Assert.That(proxy1, Is.SameAs(proxy2));
		}

		[Test]
		public void Specify_dependency_on_default_object()
		{
			_ctx.RegisterDefault<CountingType>();

			_ctx.RegisterDefault<SimpleType>()
				.DependingOnDefault<CountingType>();

			_ctx.GetObject<SimpleType>();
			Assert.That(CountingType.Count, Is.EqualTo(1));
		}

		[Test]
		public void Specify_dependency_on_named_object()
		{
			_ctx.RegisterNamed<CountingType>("counting");

			_ctx.RegisterDefault<SimpleType>()
				.DependingOn<CountingType>("counting");
			
			_ctx.GetObject<SimpleType>();
			Assert.That(CountingType.Count, Is.EqualTo(1));
		}

		[Test]
		public void Specify_dependency_on_uniquely_named_object()
		{
			var reference = _ctx.RegisterUniquelyNamed<CountingType>().GetReference();
			
			_ctx.RegisterDefault<SimpleType>()
				.DependingOn<CountingType>(reference);
			
			_ctx.GetObject<SimpleType>();
			Assert.That(CountingType.Count, Is.EqualTo(1));
		}

		[Test]
		public void Specify_multiple_dependencies()
		{
			_ctx.RegisterDefault<CountingType>();
			_ctx.RegisterDefault<OtherCountingType>();
			
			_ctx.RegisterDefault<SimpleType>()
				.DependingOnDefault<CountingType>()
				.DependingOnDefault<OtherCountingType>();

			Assert.That(CountingType.Count, Is.EqualTo(0));
			Assert.That(OtherCountingType.Count, Is.EqualTo(0));

			_ctx.GetObject<SimpleType>();

			Assert.That(CountingType.Count, Is.EqualTo(1));
			Assert.That(OtherCountingType.Count, Is.EqualTo(1));
		}
	}
}
