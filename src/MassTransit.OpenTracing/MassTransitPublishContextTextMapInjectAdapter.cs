using System;
using System.Collections;
using System.Collections.Generic;
using OpenTracing;
using OpenTracing.Propagation;

namespace MassTransit.OpenTracing
{
    public class MassTransitPublishContextTextMapInjectAdapter : ITextMap
    {
        private readonly PublishContext _context;
        public MassTransitPublishContextTextMapInjectAdapter(PublishContext context)
        {
            _context = context;
        }
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            throw new NotSupportedException(
                $"{nameof(TextMapInjectAdapter)} should only be used with {nameof(ITracer)}.{nameof(ITracer.Inject)}");
        }

        public void Set(string key, string value)
        {
            _context.Headers.Set(key, value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
