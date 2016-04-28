using System.Fabric;
using System.Fabric.Health;
using System.Web.Http.Filters;

namespace Application1.Gateway
{
    internal class GatewayExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private ServiceContext _serviceContext;

        public GatewayExceptionFilterAttribute(ServiceContext context)
        {
            _serviceContext = context;
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            ServiceEventSource.Current.ReportServiceHealth(_serviceContext, HealthState.Error, "Exception");
        }
    }
}
