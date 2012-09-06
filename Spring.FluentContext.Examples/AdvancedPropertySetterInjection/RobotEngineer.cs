using System;

namespace Spring.FluentContext.Examples.AdvancedPropertySetterInjection
{
	class RobotEngineer : IFactoryEngineer
	{
		public void Construct(string item)
		{
			Console.WriteLine("Robot RX-543 is proceeding construction of {0}.", item);
		}
	}
}