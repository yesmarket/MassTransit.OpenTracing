namespace MassTransit.OpenTracing
{
    public static class OpenTracingMiddlewareConfiguratorExtensions
    {
        public static void UseOpenTracing(this IBusFactoryConfigurator value)
        {
            value.ConfigurePublish(configurator => configurator.AddPipeSpecification(new OpenTracingPipeSpecification()));
            value.AddPipeSpecification(new OpenTracingPipeSpecification());
        }
    }
}