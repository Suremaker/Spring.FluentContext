using System;
using System.Linq.Expressions;
using Spring.FluentContext.Builders;

namespace Spring.FluentContext.BuildingStages.Objects
{
	/// <summary>
	/// Interface for object configuration build stage.
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public interface IObjectConfigurationBuildStage<TObject> : IInitBehaviorBuildStage<TObject>
	{
		/// <summary>
		/// Binds property specified by <c>propertySelector</c>.
		/// </summary>
		/// <typeparam name="TProperty">Type of property.</typeparam>
		/// <param name="propertySelector">Lambda expression to select property.</param>
		/// <returns>IPropertyDefinitionBuilder instance.</returns>
		IPropertyDefinitionBuilder<TObject, TProperty> BindProperty<TProperty>(Expression<Func<TObject, TProperty>> propertySelector);

		/// <summary>
		/// Binds property specified by <c>propertyName</c>.
		/// </summary>
		/// <typeparam name="TProperty">Type of property.</typeparam>
		/// <param name="propertyName">Property name.</param>
		/// <returns>IPropertyDefinitionBuilder instance.</returns>
		IPropertyDefinitionBuilder<TObject, TProperty> BindPropertyNamed<TProperty>(string propertyName);		
	}
}
