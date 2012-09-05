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
		}

		[Test]
		public void Register_proxy_factory()
		{
			_ctx.Register<Calculator>("calcImpl");
			_ctx.RegisterProxyFactory<ICalculator>("calc")
				.Targeting("calcImpl");

			Assert.That(_ctx.GetObject<ICalculator>("calc").Add(3, 5), Is.EqualTo(8));
		}

		[Test]
		public void Register_proxy_factory_for_typed_reference()
		{
			var reference = _ctx.Register<Calculator>().GetReference();
			_ctx.RegisterProxyFactory<ICalculator>("calc")
				.Targeting(reference);

			Assert.That(_ctx.GetObject<ICalculator>("calc").Add(3, 5), Is.EqualTo(8));
		}

		[Test]
		public void Register_proxy_factory_for_default_target()
		{
			_ctx.Register<Calculator>();
			_ctx.RegisterProxyFactory<ICalculator>("calc")
				.TargetingDefaultReference<Calculator>();

			Assert.That(_ctx.GetObject<ICalculator>("calc").Add(3, 5), Is.EqualTo(8));
		}

		[Test]
		public void Register_proxy_factory_with_named_interceptor()
		{
			_ctx.Register<Calculator>();
			_ctx.Register<MultiplingInterceptor>("interceptor");
			_ctx.RegisterProxyFactory<ICalculator>("calc").TargetingDefaultReference<Calculator>()
				.AddInterceptor("interceptor");

			Assert.That(_ctx.GetObject<ICalculator>("calc").Add(3, 5), Is.EqualTo(80));
		}

		[Test]
		public void Register_proxy_factory_with_typed_interceptor()
		{
			_ctx.Register<Calculator>();
			var interceptorRef = _ctx.Register<MultiplingInterceptor>().GetReference();
			_ctx.RegisterProxyFactory<ICalculator>("calc").TargetingDefaultReference<Calculator>()
				.AddInterceptor(interceptorRef);

			Assert.That(_ctx.GetObject<ICalculator>("calc").Add(3, 5), Is.EqualTo(80));
		}

		[Test]
		public void Register_proxy_factory_with_default_interceptor()
		{
			_ctx.Register<Calculator>();
			_ctx.Register<MultiplingInterceptor>();
			_ctx.RegisterProxyFactory<ICalculator>("calc").TargetingDefaultReference<Calculator>()
				.AddInterceptorDefaultReference<MultiplingInterceptor>();

			Assert.That(_ctx.GetObject<ICalculator>("calc").Add(3, 5), Is.EqualTo(80));
		}

		[Test]
		public void Register_default_proxy_factory()
		{
			_ctx.Register<Calculator>();
			_ctx.Register<MultiplingInterceptor>();
			_ctx.RegisterProxyFactory<ICalculator>()
				.TargetingDefaultReference<Calculator>()
				.AddInterceptorDefaultReference<MultiplingInterceptor>();

			Assert.That(_ctx.GetObject<ICalculator>().Add(3, 5), Is.EqualTo(80));
		}

		[Test]
		public void Register_unique_proxy_factory()
		{
			_ctx.Register<Calculator>();
			_ctx.Register<MultiplingInterceptor>();
			var proxyRef = _ctx.RegisterProxyFactory<ICalculator>("factory")
				.TargetingDefaultReference<Calculator>()
				.AddInterceptorDefaultReference<MultiplingInterceptor>()
				.GetReference();

			Assert.That(_ctx.GetObject<ICalculator>(proxyRef).Add(3, 5), Is.EqualTo(80));
		}
	}
}
