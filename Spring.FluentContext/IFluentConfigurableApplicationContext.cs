using Spring.Objects.Factory.Config;

namespace Spring.FluentContext
{
    public interface IFluentConfigurableApplicationContext
    {
        IConfigurableListableObjectFactory TemporaryInitObjectFactory { get; set; }
    }
}