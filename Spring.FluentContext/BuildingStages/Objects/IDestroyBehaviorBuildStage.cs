using System;
using System.Linq.Expressions;

namespace Spring.FluentContext.BuildingStages.Objects
{
	/// <summary>
	/// Interface for destroy behavior build stage.
	/// </summary>
	/// <typeparam name="TObject">Type of configured object.</typeparam>
	public interface IDestroyBehaviorBuildStage<TObject> : IValidationBuildStage<TObject>
	{
		/// <summary>
		/// Specifies method described by <c>destroyMethodSelector</c> to execute just before object is destroyed during context destruction.
		/// </summary>
		/// <param name="destroyMethodSelector">Lambda expression to select method to call.</param>
		/// <returns>Next build stage.</returns>
		IValidationBuildStage<TObject> CallOnDestroy(Expression<Action<TObject>> destroyMethodSelector);
	}
}
