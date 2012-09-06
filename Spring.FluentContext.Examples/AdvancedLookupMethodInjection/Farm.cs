using System;

namespace Spring.FluentContext.Examples.AdvancedLookupMethodInjection
{
	public abstract class Farm : IFarm
	{
		public abstract IAnimal CreateAnimal();
		public abstract IShelter CreateShelter();

		public void RunFarm()
		{
			var shelter = CreateShelter();
			while (!shelter.IsFull)
			{
				IAnimal animal = CreateAnimal();
				Console.WriteLine("Breeding {0}...", animal);
				shelter.Add(animal);
			}

			Console.WriteLine("{0} is now full of animals: {1}", shelter.GetType().Name, string.Join(", ", shelter.Animals));
		}
	}
}