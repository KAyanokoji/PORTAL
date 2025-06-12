using Microsoft.AspNetCore.Http;


namespace PORTAL.DOMAIN.Exceptions
{
    public class EntityNotFoundException(string entityName, object entityId) : ApplicationException("Entity Not Found", StatusCodes.Status404NotFound,
              $"{entityName} with ID {entityId} not found")
    {
        public string EntityName { get; } = entityName;
        public object EntityId { get; } = entityId;
    }

    public class OptimisticConcurrencyException(string entityName, object entityId) : ApplicationException("Concurrency Conflict", StatusCodes.Status409Conflict,
              $"{entityName} with ID {entityId} was modified by another user")
    {
    }

    public class DataIntegrityViolationException(string message) : ApplicationException("Data Integrity Violation", StatusCodes.Status400BadRequest, message)
    {
    }


    public class DuplicateDataException(string entityName, object entityId) : ApplicationException("Duplicate Data", StatusCodes.Status409Conflict,
             $"{entityName} with key {entityId} already exists")
    { }
}
