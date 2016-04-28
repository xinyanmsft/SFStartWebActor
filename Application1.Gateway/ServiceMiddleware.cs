using Microsoft.Owin;
using System.Diagnostics;
using System.Fabric;
using System.Threading.Tasks;

namespace Application1.Gateway
{
    internal class ServiceMiddleware : OwinMiddleware
    {
        public ServiceMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            if (this.Next == null)
                return;

            Stopwatch s = new Stopwatch();
            s.Start();

            try
            {
                ServiceEventSource.Current.ServiceRequestStart(context.Request.Path.ToString());

                context.Response.OnSendingHeaders(state =>
                {
                    context.Response.Headers["Cache-Control"] = "no-cache";
                }, context);

                await this.Next.Invoke(context);
            }
            finally
            {
                s.Stop();
                ServiceEventSource.Current.ServiceRequestStop(context.Request.Path.ToString(), s.ElapsedMilliseconds);
            }
        }
    }
}
