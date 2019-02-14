using Spring.Context;
using Spring.FluentContext.Examples.VariousCreationMethods.Objects;

namespace Spring.FluentContext.Examples.VariousCreationMethods
{
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
			ctx.GetDefaultObject<IWindow>().SimulateGuiActions();
		}
	}
}

