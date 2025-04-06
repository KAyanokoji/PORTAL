using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTAL.DOMAIN.Exceptions
{
    public class RateLimitExceededException : ApplicationException
    {
        public TimeSpan RetryAfter { get; }

        public RateLimitExceededException(TimeSpan retryAfter)
            : base("Too Many Requests", StatusCodes.Status429TooManyRequests,
                  $"Rate limit exceeded. Please try again in {retryAfter.TotalSeconds} seconds.")
        {
            RetryAfter = retryAfter;
        }
    }
}
