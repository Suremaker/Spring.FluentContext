using System;
using System.Linq.Expressions;
using Spring.FluentContext.Builders;

namespace Spring.FluentContext.BuildingStages.Objects
{
	/// <summary>
	/// Interface for method configuration build stage.
	/// </summary>
	/// <typeparam name="TObject">Type of configured object.</typeparam>
	public interface IMethodConfigurationBuildStage<TObject> : IAutoConfigurationBuildStage<TObject>
	{
		/// <summary>
		/// Binds lookup method specified by <c>methodSelector</c>.
		/// </summary>
		/// <typeparam name="TResult">Method return type.</typeparam>
		/// <param name="methodSelector">Lambda expression to select method to call.</param>
		/// <returns>ILookupMethodDefinitionBuilder instance.</returns>
		ILookupMethodDefinitionBuilder<TObject, TResult> BindLookupMethod<TResult>(Expression<Func<TObject, TResult>> methodSelector);

		/// <summary>
		/// Binds lookup method specified by <c>methodName</c>.
		/// </summary>
		/// <typeparam name="TResult">Method return type.</typeparam>
		/// <param name="methodName">Method name.</param>
		/// <returns>ILookupMethodDefinitionBuilder instance.</returns>
		ILookupMethodDefinitionBuilder<TObject, TResult> BindLookupMethodNamed<TResult>(string methodName);
	}
}