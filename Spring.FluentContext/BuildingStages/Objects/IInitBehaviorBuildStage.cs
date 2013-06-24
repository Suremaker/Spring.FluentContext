using System;
using System.Linq.Expressions;

namespace Spring.FluentContext.BuildingStages.Objects
{
	/// <summary>
	/// Interface for init behavior build stage.
	/// </summary>
	/// <typeparam name="TObject">Type of configured object.</typeparam>
	public interface IInitBehaviorBuildStage<TObject> : IDestroyBehaviorBuildStage<TObject>
	{
		/// <summary>
		/// Specifies method described by <c>initMethodSelector</c> to execute just after object is created and configured.
		/// </summary>
		/// <param name="initMethodSelector">Lambda expression to select method to call.</param>
		/// <returns>Next build stage.</returns>
		IDestroyBehaviorBuildStage<TObject> CallOnInit(Expression<Action<TObject>> initMethodSelector);
	}

}
