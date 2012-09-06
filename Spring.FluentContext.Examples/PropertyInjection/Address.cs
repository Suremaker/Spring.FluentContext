namespace Spring.FluentContext.Examples.PropertyInjection
{
	class Address
	{
		public string Street { get; set; }
		public string PostCode { get; set; }
		public string City { get; set; }

		public override string ToString()
		{
			return string.Format("{0}, {1}, {2}", Street, PostCode, City);
		}
	}
}