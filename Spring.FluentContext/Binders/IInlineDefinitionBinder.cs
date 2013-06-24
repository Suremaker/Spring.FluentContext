using System;
using Spring.FluentContext.BuildingStages.Objects;

namespace Spring.FluentContext.Binders
{
	/// <summary>
	/// Interface for inline definition binder.
	/// </summary>
	/// <typeparam name="TBuilder">Type of builder returned after binding is done.</typeparam>
	/// <typeparam name="TTargetType">Type of binding.</typeparam>
	public interface IInlineDefinitionBinder<out TBuilder, in TTargetType>
	{

		/// <summary>
		/// Specifies an inline object definition as binding target, where targeted object is defined by <c>innerObjectBuildAction</c> action.
		/// </summary>
		/// <typeparam name="TInnerObject">Type of object that would be instantiated.</typeparam>
		/// <param name="innerObjectBuildAction">Inline definition build action.</param>
		/// <returns>Builder.</returns>
		TBuilder ToInlineDefinition<TInnerObject>(Action<IInstantiationBuildStage<TInnerObject>> innerObjectBuildAction) where TInnerObject : TTargetType;

		/// <summary>
		/// Specifies an inline, default object definition as binding target.
		/// This method is useful if specific implementation has to be injected, where it does not require additional configuration.
		/// </summary>
		/// <typeparam name="TInnerObject">Type of object that would be instantiated.</typeparam>
		/// <returns>Builder.</returns>
		TBuilder ToInlineDefinition<TInnerObject>() where TInnerObject : TTargetType;
	}
}