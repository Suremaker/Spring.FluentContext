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
	}
}
