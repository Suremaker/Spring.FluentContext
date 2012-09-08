//
//  Author:
//    Wojciech Kotlarski
//
//  Copyright (c) 2012, Wojciech Kotlarski
//
//  All rights reserved.
//
//  Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.	 
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in
//       the documentation and/or other materials provided with the distribution.
//
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
//  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT 
//  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS 
//  FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR 
//  CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, 
//  EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, 
//  PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF 
//  LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
//  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
//  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
using Spring.Context;
using System;

namespace Spring.FluentContext.Examples.VariousCreationMethods
{
	class Button
	{
		private Action<Button> _clickAction;

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

	interface IWindow
	{
		void SimulateGuiActions();
	}

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

	class VariousCreationMethodsExample : Example
	{
		protected override IApplicationContext CreateContext()
		{
			var ctx = new FluentApplicationContext();
			ctx.RegisterDefault<ButtonFactory>().UseStaticFactoryMethod(ButtonFactory.CreateInstance);

			var closeBt = ctx.RegisterUniquelyNamed<Button>()
				.UseFactoryMethod<ButtonFactory>(f => f.CreateButton()).OfRegisteredDefault()
				.BindProperty(b => b.Name).ToValue("Close")
				.GetReference();

			var saveBt = ctx.RegisterUniquelyNamed<Button>()
				.UseFactoryMethod<ButtonFactory>(f => f.CreateButton()).OfRegisteredDefault()
				.BindProperty(b => b.Name).ToValue("Save")
				.GetReference();

			ctx.RegisterDefault<Window>()
				.BindProperty(w => w.CloseButton).ToRegistered(closeBt)
				.BindProperty(w => w.SaveButton).ToRegistered(saveBt);

			ctx.RegisterDefaultAlias<IWindow>().ToRegisteredDefault<Window>();

			return ctx;
		}
		
		protected override void RunExample(IApplicationContext ctx)
		{
			ctx.GetObject<IWindow>().SimulateGuiActions();
		}
	}
}

