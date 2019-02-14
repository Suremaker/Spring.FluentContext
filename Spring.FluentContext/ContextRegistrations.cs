using System.Reflection;
using Spring.Context;
using Spring.Context.Support;
using Spring.FluentContext.BuildingStages.Aliases;
using Spring.FluentContext.BuildingStages.Objects;
using Spring.FluentContext.BuildingStages.ProxyFactories;
using Spring.FluentContext.Definitions;
using Spring.FluentContext.Impl;
using Spring.FluentContext.Utils;
using Spring.Objects.Factory.Config;
using Spring.Validation;
using Spring.Validation.Actions;

namespace Spring.FluentContext
{
    public static class ContextRegistrations
    {
        		/// <summary>
		/// Registers alias with default id for object of <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type of object returned by alias.</typeparam>
		/// <returns>Next build stage.</returns>
		public static IAliasLinkingBuildStage<T> RegisterDefaultAlias<T>(this AbstractApplicationContext ctx)
		{
			return RegisterNamedAlias<T>(ctx, IdGenerator<T>.GetDefaultId());
		}

		/// <summary>
		/// Registers alias with unique id for object of <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type of object returned by alias.</typeparam>
		/// <returns>Next build stage.</returns>
		public static IAliasLinkingBuildStage<T> RegisterUniquelyNamedAlias<T>(this AbstractApplicationContext ctx)
		{
			return RegisterNamedAlias<T>(ctx, IdGenerator<T>.GetUniqueId());
		}

		/// <summary>
		/// Registers alias with <c>id</c> for object of <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type of object returned by alias.</typeparam>
		/// <param name="id">Alias id.</param>
		/// <returns>Next build stage.</returns>
		public static IAliasLinkingBuildStage<T> RegisterNamedAlias<T>(this AbstractApplicationContext ctx, string id)
		{
			return new AliasDefinitionBuilder<T>(ctx, id);
		}

		/// <summary>
		/// Registers object definition with specified <c>id</c> for <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type of configured object.</typeparam>
		/// <param name="id">Object id.</param>
		/// <returns>Next build stage.</returns>
		public static IScopeBuildStage<T> RegisterNamed<T>(this IConfigurableApplicationContext ctx, string id)
		{
			var builder = new ObjectDefinitionBuilder<T>(id);
			SafeGetObjectFactory(ctx).RegisterObjectDefinition(id, builder.Definition);
			return builder;
		}

		/// <summary>
		/// Registers object definition with default id for <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type of configured object.</typeparam>
		/// <returns>Next build stage.</returns>
		public static IScopeBuildStage<T> RegisterDefault<T>(this IConfigurableApplicationContext ctx)
		{
			return RegisterNamed<T>(ctx, IdGenerator<T>.GetDefaultId());
		}

		/// <summary>
		/// Registers object definition with unique id for <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type of configured object.</typeparam>
		/// <returns>Next build stage.</returns>
		public static IScopeBuildStage<T> RegisterUniquelyNamed<T>(this IConfigurableApplicationContext ctx)
		{
			return RegisterNamed<T>(ctx, IdGenerator<T>.GetUniqueId());
		}

		/// <summary>
		/// Registers proxy factory with specified <c>id</c> for proxies of <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type to proxy.</typeparam>
		/// <param name="id">Proxy definition id.</param>
		/// <returns>Next build stage.</returns>
		public static IProxyTargetDefinitionBuildStage<T> RegisterNamedProxyFactory<T>(this IConfigurableApplicationContext ctx, string id)
		{
			var builder = new ProxyFactoryDefinitionBuilder<T>(id);
			SafeGetObjectFactory(ctx).RegisterObjectDefinition(id, builder.Definition);
			return builder;
		}

		/// <summary>
		/// Registers proxy factory with default id for proxies of <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type to proxy.</typeparam>
		/// <returns>Next build stage.</returns>
		public static IProxyTargetDefinitionBuildStage<T> RegisterDefaultProxyFactory<T>(this IConfigurableApplicationContext ctx)
		{
			return RegisterNamedProxyFactory<T>(ctx, IdGenerator<T>.GetDefaultId());
		}

		/// <summary>
		/// Registers proxy factory with unique id for proxies of <c>T</c> type.
		/// </summary>
		/// <typeparam name="T">Type to proxy.</typeparam>
		/// <returns>Next build stage.</returns>
		public static IProxyTargetDefinitionBuildStage<T> RegisterUniquelyNamedProxyFactory<T>(this IConfigurableApplicationContext ctx)
		{
			return RegisterNamedProxyFactory<T>(ctx, IdGenerator<T>.GetUniqueId());
		}

		/// <summary>
		/// Registers singleton <c>instance</c> with specified <c>id</c>.
		/// </summary>
		/// <typeparam name="T">Type of singleton instance.</typeparam>
		/// <param name="id">Id of registered object.</param>
		/// <param name="instance">Singleton instance to register.</param>
		/// <returns>Registered object reference.</returns>
		public static IObjectRef<T> RegisterNamedSingleton<T>(this IConfigurableApplicationContext ctx, string id, T instance)
		{
			SafeGetObjectFactory(ctx).RegisterSingleton(id, instance);
			return new ObjectRef<T>(id);
		}

		/// <summary>
		/// Registers singleton <c>instance</c> with default id.
		/// </summary>
		/// <typeparam name="T">Type of singleton instance.</typeparam>		
		/// <param name="instance">Singleton instance to register.</param>
		/// <returns>Registered object reference.</returns>
		public static IObjectRef<T> RegisterDefaultSingleton<T>(this IConfigurableApplicationContext ctx, T instance)
		{
			return RegisterNamedSingleton(ctx, IdGenerator<T>.GetDefaultId(), instance);
		}

		/// <summary>
		/// Registers singleton <c>instance</c> with unique id.
		/// </summary>
		/// <typeparam name="T">Type of singleton instance.</typeparam>		
		/// <param name="instance">Singleton instance to register.</param>
		/// <returns>Registered object reference.</returns>
		public static IObjectRef<T> RegisterUniquelyNamedSingleton<T>(this IConfigurableApplicationContext ctx, T instance)
		{
			return RegisterNamedSingleton(ctx, IdGenerator<T>.GetUniqueId(), instance);
		}
		
		/// <summary>
		/// Given a object name, create an alias. We typically use this method to
		/// support names that are illegal within XML ids (used for object names).
		/// </summary>
		/// <param name="name">The name of the object.</param>
		/// <param name="theAlias">The alias that will behave the same as the object name.</param>
		/// <exception cref="Spring.Objects.Factory.NoSuchObjectDefinitionException">
		/// If there is no object with the given name.
		/// </exception>
		/// <exception cref="Spring.Objects.Factory.ObjectDefinitionStoreException">
		/// If the alias is already in use.
		/// </exception>
		public static void RegisterAlias(this IConfigurableApplicationContext ctx, string name, string theAlias)
		{
			SafeGetObjectFactory(ctx).RegisterAlias(name, theAlias);
		}
		
		/// <summary>
		/// Protects access to the internal object factory used by the ApplicationContext if attempted to be accessed when in improper state.
		/// </summary>
		/// <returns>The internal ObjectFactory used by the ApplicationContext</returns>
		/// <exception cref="System.InvalidOperationException">Cannot Access ApplicationContext in this state!</exception>
		private static IConfigurableListableObjectFactory SafeGetObjectFactory(IConfigurableApplicationContext ctx)
		{
			if (ctx is IFluentConfigurableApplicationContext fcac)
			{
				return fcac.TemporaryInitObjectFactory;
			}

			if (ctx is AbstractApplicationContext)
			{
				return (IConfigurableListableObjectFactory) typeof(AbstractApplicationContext)
					.GetMethod("SafeGetObjectFactory", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(ctx, null);
			}
			else
			{
				return ctx.ObjectFactory;
			}
		}
		
		/// <summary>
		/// Appends a validation error message to Spring.Validation framework 
		/// </summary>
		public static IObjectConfigurationBuildStage<T> AddError<T>(this IObjectConfigurationBuildStage<T> builder,
			string id) where T : BaseValidator
		{
			return builder.AddMessage(id, "error");
		}

		/// <summary>
		/// Appends a validation message to Spring.Validation framework 
		/// </summary>
		public static IObjectConfigurationBuildStage<T> AddMessage<T>(this IObjectConfigurationBuildStage<T> builder,
			string id,
			params string[] providers) where T : BaseValidator
		{
			return builder.BindProperty(x => x.Actions)
				.ToList(Def.Value<IValidationAction>(new ErrorMessageAction(id, providers)));
		}
    }
}