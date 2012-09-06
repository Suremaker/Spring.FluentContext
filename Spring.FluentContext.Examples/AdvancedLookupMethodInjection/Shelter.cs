using System.Collections.Generic;

namespace Spring.FluentContext.Examples.AdvancedLookupMethodInjection
{
	class Shelter : IShelter
	{
		private readonly int _max;
		private readonly List<IAnimal> _list = new List<IAnimal>();

		protected Shelter(int max)
		{
			_max = max;
		}

		public bool IsFull { get { return _list.Count >= _max; } }
		public IEnumerable<IAnimal> Animals { get { return _list; } }
		public void Add(IAnimal animal)
		{
			_list.Add(animal);
		}
	}
}