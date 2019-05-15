# MassTransit.OpenTracing

Use this to configure trace propagation between asynchronous message broker operations.

Usage is as follows;
```
var bus1 = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
   var host = cfg.Host(new Uri("rabbitmq://localhost"), h =>
   {
      h.Username(brokerSettings.UserName);
      h.Password(brokerSettings.Password);
   });

   cfg.UseOpenTracing();
});
```

To install from nuget;
```
Install-Package MassTransit.OpenTracing
```
