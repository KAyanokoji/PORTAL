using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTAL.DOMAIN.Exceptions
{
    public class EntityNotFoundException : ApplicationException
    {
        public string EntityName { get; }
        public object EntityId { get; }

        public EntityNotFoundException(string entityName, object entityId)
            : base("Entity Not Found", StatusCodes.Status404NotFound,
                  $"{entityName} with ID {entityId} not found")
        {
            EntityName = entityName;
            EntityId = entityId;
        }
    }

    public class OptimisticConcurrencyException : ApplicationException
    {
        public OptimisticConcurrencyException(string entityName, object entityId)
            : base("Concurrency Conflict", StatusCodes.Status409Conflict,
                  $"{entityName} with ID {entityId} was modified by another user")
        { }
    }

    public class DataIntegrityViolationException : ApplicationException
    {
        public DataIntegrityViolationException(string message)
            : base("Data Integrity Violation", StatusCodes.Status400BadRequest, message) { }
    }
}
