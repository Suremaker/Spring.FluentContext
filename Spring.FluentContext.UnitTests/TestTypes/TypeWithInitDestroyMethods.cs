namespace Spring.FluentContext.UnitTests.TestTypes
{

	public class TypeWithInitDestroyMethods
	{
		public InitDestroyCallReport Report{ get; set; }

		public void Init()
		{
			Report.InitCalled = true;
		}

		public void Destroy()
		{
			Report.DestroyCalled = true;
		}
	}
}

