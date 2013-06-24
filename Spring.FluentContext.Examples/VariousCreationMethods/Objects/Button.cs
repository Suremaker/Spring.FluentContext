using System;

namespace Spring.FluentContext.Examples.VariousCreationMethods.Objects
{
	class Button
	{
		private readonly Action<Button> _clickAction;

		public string Name { get; set; }

		public Button(Action<Button> clickAction)
		{
			_clickAction = clickAction;
		}

		public void Click()
		{
			_clickAction(this);
		}
	}
}