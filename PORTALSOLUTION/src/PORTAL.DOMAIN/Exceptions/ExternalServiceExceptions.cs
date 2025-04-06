using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTAL.DOMAIN.Exceptions
{
    public class ExternalServiceException : ApplicationException
    {
        public string ServiceName { get; }
        public int? ExternalStatusCode { get; }

        public ExternalServiceException(string serviceName, string message, int? externalStatusCode = null)
            : base("External Service Error",
                  externalStatusCode.HasValue ? externalStatusCode.Value : StatusCodes.Status502BadGateway,
                  $"{serviceName} error: {message}")
        {
            ServiceName = serviceName;
            ExternalStatusCode = externalStatusCode;
        }
    }

    public class ThirdPartyApiException : ExternalServiceException
    {
        public ThirdPartyApiException(string serviceName, string message, int? externalStatusCode = null)
            : base(serviceName, message, externalStatusCode) { }
    }

    public class PaymentGatewayException : ExternalServiceException
    {
        public PaymentGatewayException(string message, int? externalStatusCode = null)
            : base("Payment Gateway", message, externalStatusCode) { }
    }
}
