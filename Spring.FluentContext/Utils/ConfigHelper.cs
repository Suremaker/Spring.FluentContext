using Spring.FluentContext.BuildingStages.Objects;
using Spring.FluentContext.Definitions;
using Spring.Validation;
using Spring.Validation.Actions;

namespace Spring.FluentContext.Utils
{
    public static class ConfigHelper
    {
        public static IObjectConfigurationBuildStage<T> AddError<T>(this IObjectConfigurationBuildStage<T> builder,
            string id) where T : BaseValidator
        {
            return builder.AddMessage(id, "error");
        }

        public static IObjectConfigurationBuildStage<T> AddMessage<T>(this IObjectConfigurationBuildStage<T> builder,
            string id,
            params string[] providers) where T : BaseValidator
        {
            return builder.BindProperty(x => x.Actions)
                .ToList(Def.Value<IValidationAction>(new ErrorMessageAction(id, providers)));
        }
    }
}