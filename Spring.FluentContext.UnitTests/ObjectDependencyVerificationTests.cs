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

