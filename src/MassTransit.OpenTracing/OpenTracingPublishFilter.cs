using System.Threading.Tasks;
using GreenPipes;
using OpenTracing.Propagation;
using OpenTracing.Util;

namespace MassTransit.OpenTracing
{
   public class OpenTracingPublishFilter : IFilter<PublishContext>
   {
      public void Probe(ProbeContext context)
      { }

      public async Task Send(PublishContext context, IPipe<PublishContext> next)
      {
         var operationName = $"Publishing Message: {context.DestinationAddress.GetExchangeName()}";

         var spanBuilder = GlobalTracer.Instance.BuildSpan(operationName);
         var currentCtx = GlobalTracer.Instance.ActiveSpan?.Context;

         if (currentCtx != null)
         {
            spanBuilder.AsChildOf(GlobalTracer.Instance.ActiveSpan.Context);
         }


         spanBuilder.WithTag("destination-address", context.DestinationAddress?.ToString())
                    .WithTag("source-address", context.SourceAddress?.ToString())
                    .WithTag("initiator-id", context.InitiatorId?.ToString())
                    .WithTag("message-id", context.MessageId?.ToString());

         using (var scope = spanBuilder.StartActive())
         {
            GlobalTracer.Instance.Inject(scope.Span.Context, BuiltinFormats.TextMap, new MassTransitTextMapInjectAdapter(context));
            await next.Send(context);
         }
      }
   }
}
