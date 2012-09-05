using System;
using System.Collections.Generic;
using Spring.Context;

namespace Spring.FluentContext.Examples
{
	public interface IAnimal { }
	public interface IShelter
	{
		bool IsFull { get; }
		IEnumerable<IAnimal> Animals { get; }
		void Add(IAnimal animal);
	}

	interface IFarm
	{
		void RunFarm();
	}

	class Pig : IAnimal
	{
		public override string ToString()
		{
			return GetType().Name;
		}
	}

	class Cow : IAnimal
	{
		public override string ToString()
		{
			return GetType().Name;
		}
	}

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

	class Pigstry : Shelter { Pigstry() : base(5) { } }
	class Barn : Shelter { Barn() : base(2) { } }

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
				Console.WriteLine("Breeding new {0}...", animal);
				shelter.Add(animal);
			}

			Console.WriteLine("{0} is now full of animals: {1}", shelter.GetType().Name, string.Join(", ", shelter.Animals));
		}
	}

	internal class AdvancedLookupMethodInjection : Example
	{
		protected override IApplicationContext CreateContext()
		{
			var ctx = new FluentApplicationContext();

			ctx.RegisterDefault<Pig>().AsPrototype();
			ctx.RegisterDefault<Cow>().AsPrototype();
			ctx.RegisterDefault<Pigstry>().AsSingleton();
			ctx.RegisterDefault<Barn>().AsPrototype();

			ctx.RegisterNamed<Farm>("pigFarm")
				.BindLookupMethod(f => f.CreateAnimal()).ToRegisteredDefaultOfType<Pig>()
				.BindLookupMethod(f => f.CreateShelter()).ToRegisteredDefaultOfType<Pigstry>();

			ctx.RegisterNamed<Farm>("cowFarm")
				.BindLookupMethod(f => f.CreateAnimal()).ToRegisteredDefaultOfType<Cow>()
				.BindLookupMethod(f => f.CreateShelter()).ToRegisteredDefaultOfType<Barn>();

			return ctx;
		}

		protected override void RunExample(IApplicationContext ctx)
		{
			IFarm cowFarm = ctx.GetObject<IFarm>("cowFarm");
			IFarm pigFarm = ctx.GetObject<IFarm>("pigFarm");

			Console.WriteLine("\nRunning cow farm");
			cowFarm.RunFarm();

			Console.WriteLine("\nRunning pig farm");
			pigFarm.RunFarm();

			Console.WriteLine("\nRunning cow farm for second time");
			cowFarm.RunFarm();

			Console.WriteLine("\nRunning pig farm for second time");
			pigFarm.RunFarm();
		}
	}
}