using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTAL.DOMAIN.Exceptions
{
    public class BusinessRuleViolationException : ApplicationException
    {
        public string RuleName { get; }

        public BusinessRuleViolationException(string ruleName, string message)
            : base("Business Rule Violation", StatusCodes.Status422UnprocessableEntity, message)
        {
            RuleName = ruleName;
        }
    }

    public class ConcurrentModificationException : ApplicationException
    {
        public ConcurrentModificationException(string resourceName)
            : base("Concurrent Modification", StatusCodes.Status409Conflict,
                  $"The {resourceName} was modified by another user. Please refresh and try again.")
        { }
    }

    public class InvalidStateTransitionException : ApplicationException
    {
        public string CurrentState { get; }
        public string AttemptedState { get; }

        public InvalidStateTransitionException(string currentState, string attemptedState)
            : base("Invalid State Transition", StatusCodes.Status400BadRequest,
                  $"Cannot transition from {currentState} to {attemptedState}")
        {
            CurrentState = currentState;
            AttemptedState = attemptedState;
        }
    }
}
