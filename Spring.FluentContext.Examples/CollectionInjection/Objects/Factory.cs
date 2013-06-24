namespace Spring.FluentContext.Examples.CollectionInjection.Objects
{
	class Factory
	{
		private readonly IWorker[] _workers;

		public Factory(IWorker[] workers)
		{
			_workers = workers;
		}

		public void Create(string thing)
		{
			foreach (var worker in _workers)
				worker.WorkOn(thing);
		}
	}
}