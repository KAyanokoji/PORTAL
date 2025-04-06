using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTAL.DOMAIN.Exceptions
{
    public class ModelValidationException : ValidationException
    {
        public ModelValidationException(IDictionary<string, string[]> errors)
            : base("Invalid Model State", StatusCodes.Status400BadRequest, errors) { }
    }

    public class CommandValidationException : ValidationException
    {
        public CommandValidationException(IDictionary<string, string[]> errors)
            : base("Invalid Command", StatusCodes.Status400BadRequest, errors) { }
    }

    public class QueryValidationException : ValidationException
    {
        public QueryValidationException(IDictionary<string, string[]> errors)
            : base("Invalid Query", StatusCodes.Status400BadRequest, errors) { }
    }
}
