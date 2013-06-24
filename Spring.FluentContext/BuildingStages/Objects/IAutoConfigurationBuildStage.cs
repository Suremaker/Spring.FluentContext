using Spring.Objects.Factory.Config;

namespace Spring.FluentContext.BuildingStages.Objects
{
	/// <summary>
	/// Interface for auto configuration build stage.
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public interface IAutoConfigurationBuildStage<TObject> : IObjectConfigurationBuildStage<TObject>
	{
		/// <summary>
		/// Autowires object dependencies using AutoWiringMode.AutoDetect mode.
		/// </summary>
		/// <returns></returns>
		IObjectConfigurationBuildStage<TObject> Autowire();

		/// <summary>
		/// Autowires object dependencies using specified <c>mode</c>.
		/// </summary>
		/// <param name="mode"></param>
		/// <returns></returns>
		IObjectConfigurationBuildStage<TObject> Autowire(AutoWiringMode mode);
	}
}
