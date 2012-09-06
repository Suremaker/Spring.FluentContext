using System;
using Spring.FluentContext.Examples.AdvancedLookupMethodInjection;
using Spring.FluentContext.Examples.AdvancedPropertySetterInjection;
using Spring.FluentContext.Examples.Complex;
using Spring.FluentContext.Examples.ConstructorInjection;
using Spring.FluentContext.Examples.LookupMethodInjection;
using Spring.FluentContext.Examples.PropertyInjection;
using Spring.FluentContext.Examples.ProxyFactoryUsage;

namespace Spring.FluentContext.Examples
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Usage examples of Spring.FluentContext:\n---------------------------------------");
			var examples = new Example[]
				{
					new ConstructorInjectionExample(), 
					new PropertyInjectionExample(),
					new LookupMethodInjectionExample(), 
					new AdvancedPropertySetterInjectionExample(), 					
					new AdvancedLookupMethodInjectionExample(),
					new ProxyFactoryUsageExample(),
					new ComplexExample()
				};

			foreach(var example in examples)
				example.Show();
		}
	}
}
