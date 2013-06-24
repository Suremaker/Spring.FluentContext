using NUnit.Framework;
using Spring.FluentContext.UnitTests.TestTypes;

namespace Spring.FluentContext.UnitTests
{
	[TestFixture]
	public class ObjectInitDestroyMethodTests
	{
		private FluentApplicationContext _ctx;
			
		[SetUp]
		public void SetUp()
		{
			_ctx = new FluentApplicationContext();
		}

		[Test]
		public void Use_init_method()
		{
			_ctx.RegisterDefault<InitDestroyCallReport>();

			_ctx.RegisterDefault<TypeWithInitDestroyMethods>()
				.Autowire()
				.CallOnInit(t => t.Init());

			_ctx.GetObject<TypeWithInitDestroyMethods>();
			Assert.That(_ctx.GetObject<InitDestroyCallReport>().InitCalled);
		}

		[Test]
		public void Use_destroy_method()
		{
			_ctx.RegisterDefault<InitDestroyCallReport>();
			
			_ctx.RegisterDefault<TypeWithInitDestroyMethods>()
				.Autowire()
				.CallOnDestroy(t => t.Destroy());
			
			_ctx.GetObject<TypeWithInitDestroyMethods>();
			var report = _ctx.GetObject<InitDestroyCallReport>();
			_ctx.Dispose();
			Assert.That(report.DestroyCalled);
		}
	}
}

