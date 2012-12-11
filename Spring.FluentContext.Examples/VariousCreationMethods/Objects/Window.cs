namespace Spring.FluentContext.Examples.VariousCreationMethods.Objects
{
	class Window : IWindow
	{
		public Button CloseButton { get; set; }

		public Button SaveButton { get; set; }

		public void SimulateGuiActions()
		{
			SaveButton.Click();
			CloseButton.Click();
		}
	}
}