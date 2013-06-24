using System;

namespace Spring.FluentContext.Examples.VariousCreationMethods.Objects
{
	class ButtonFactory
	{
		public static ButtonFactory CreateInstance()
		{
			return new ButtonFactory();
		}

		public Button CreateButton()
		{
			return new Button(b => Console.WriteLine("Button {0} clicked...", b.Name));
		}

		private ButtonFactory()
		{
		}
	}
}