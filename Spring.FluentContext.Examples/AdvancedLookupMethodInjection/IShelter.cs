using System.Collections.Generic;

namespace Spring.FluentContext.Examples.AdvancedLookupMethodInjection
{
	public interface IShelter
	{
		bool IsFull { get; }
		IEnumerable<IAnimal> Animals { get; }
		void Add(IAnimal animal);
	}
}