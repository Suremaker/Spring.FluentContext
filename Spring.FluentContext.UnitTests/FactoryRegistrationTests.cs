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
				.TargetingDefault<Calculator>();

			Assert.That(_ctx.GetObject<ICalculator>("calc").Add(3, 5), Is.EqualTo(8));
		}

		[Test]
		public void Register_proxy_factory_with_named_interceptor()
		{
			_ctx.RegisterDefault<Calculator>();
			_ctx.RegisterNamed<MultiplingInterceptor>("interceptor");
			_ctx.RegisterNamedProxyFactory<ICalculator>("calc").TargetingDefault<Calculator>()
				.InterceptedBy("interceptor");

			Assert.That(_ctx.GetObject<ICalculator>("calc").Add(3, 5), Is.EqualTo(80));
		}

		[Test]
		public void Register_proxy_factory_with_typed_interceptor()
		{
			_ctx.RegisterDefault<Calculator>();
			var interceptorRef = _ctx.RegisterDefault<MultiplingInterceptor>().GetReference();
			_ctx.RegisterNamedProxyFactory<ICalculator>("calc").TargetingDefault<Calculator>()
				.InterceptedBy(interceptorRef);

			Assert.That(_ctx.GetObject<ICalculator>("calc").Add(3, 5), Is.EqualTo(80));
		}

		[Test]
		public void Register_proxy_factory_with_default_interceptor()
		{
			_ctx.RegisterDefault<Calculator>();
			_ctx.RegisterDefault<MultiplingInterceptor>();
			_ctx.RegisterNamedProxyFactory<ICalculator>("calc").TargetingDefault<Calculator>()
				.InterceptedByDefault<MultiplingInterceptor>();

			Assert.That(_ctx.GetObject<ICalculator>("calc").Add(3, 5), Is.EqualTo(80));
		}

		[Test]
		public void Register_default_proxy_factory()
		{
			_ctx.RegisterDefault<Calculator>();
			_ctx.RegisterDefault<MultiplingInterceptor>();
			_ctx.RegisterDefaultProxyFactory<ICalculator>()
				.TargetingDefault<Calculator>()
				.InterceptedByDefault<MultiplingInterceptor>();

			Assert.That(_ctx.GetDefaultObject<ICalculator>().Add(3, 5), Is.EqualTo(80));
		}

		[Test]
		public void Register_unique_proxy_factory()
		{
			_ctx.RegisterDefault<Calculator>();
			_ctx.RegisterDefault<MultiplingInterceptor>();
			var proxyRef = _ctx.RegisterNamedProxyFactory<ICalculator>("factory")
				.TargetingDefault<Calculator>()
				.InterceptedByDefault<MultiplingInterceptor>()
				.GetReference();

			Assert.That(_ctx.GetObject(proxyRef).Add(3, 5), Is.EqualTo(80));
		}

		[Test]
		public void Register_proxy_factory_to_return_prototypes()
		{
			_ctx.RegisterDefault<Calculator>();
			_ctx.RegisterDefaultProxyFactory<ICalculator>()
				.TargetingDefault<Calculator>()
				.ReturningPrototypes();

			var proxy1 = _ctx.GetDefaultObject<ICalculator>();
			var proxy2 = _ctx.GetDefaultObject<ICalculator>();

			Assert.That(proxy1, Is.Not.SameAs(proxy2));
		}

		[Test]
		public void Register_proxy_factory_to_return_singleton()
		{
			_ctx.RegisterDefault<Calculator>();
			_ctx.RegisterDefaultProxyFactory<ICalculator>()
				.TargetingDefault<Calculator>()
				.ReturningSingleton();
			
			var proxy1 = _ctx.GetDefaultObject<ICalculator>();
			var proxy2 = _ctx.GetDefaultObject<ICalculator>();
			
			Assert.That(proxy1, Is.SameAs(proxy2));
		}

		[Test]
		public void Proxy_factory_returns_singleton_by_default()
		{
			_ctx.RegisterDefault<Calculator>();
			_ctx.RegisterDefaultProxyFactory<ICalculator>()
				.TargetingDefault<Calculator>();
			
			var proxy1 = _ctx.GetDefaultObject<ICalculator>();
			var proxy2 = _ctx.GetDefaultObject<ICalculator>();
			
			Assert.That(proxy1, Is.SameAs(proxy2));
		}

		[Test]
		public void Specify_dependency_on_default_object()
		{
			_ctx.RegisterDefault<CountingType>();

			_ctx.RegisterDefault<SimpleType>()
				.DependingOnDefault<CountingType>();

			_ctx.GetDefaultObject<SimpleType>();
			Assert.That(CountingType.Count, Is.EqualTo(1));
		}

		[Test]
		public void Specify_dependency_on_named_object()
		{
			_ctx.RegisterNamed<CountingType>("counting");

			_ctx.RegisterDefault<SimpleType>()
				.DependingOn<CountingType>("counting");
			
			_ctx.GetDefaultObject<SimpleType>();
			Assert.That(CountingType.Count, Is.EqualTo(1));
		}

		[Test]
		public void Specify_dependency_on_uniquely_named_object()
		{
			var reference = _ctx.RegisterUniquelyNamed<CountingType>().GetReference();
			
			_ctx.RegisterDefault<SimpleType>()
				.DependingOn<CountingType>(reference);
			
			_ctx.GetDefaultObject<SimpleType>();
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

			_ctx.GetDefaultObject<SimpleType>();

			Assert.That(CountingType.Count, Is.EqualTo(1));
			Assert.That(OtherCountingType.Count, Is.EqualTo(1));
		}
	}
}
