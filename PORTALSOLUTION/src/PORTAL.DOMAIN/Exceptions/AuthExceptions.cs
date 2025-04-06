using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTAL.DOMAIN.Exceptions
{
    public class AuthenticationFailedException : ApplicationException
    {
        public AuthenticationFailedException(string message = "Invalid username or password")
            : base("Authentication Failed", StatusCodes.Status401Unauthorized, message) { }
    }

    public class AccountLockedException : ApplicationException
    {
        public AccountLockedException(string message = "Account is temporarily locked")
            : base("Account Locked", StatusCodes.Status403Forbidden, message) { }
    }

    public class InvalidTokenException : ApplicationException
    {
        public InvalidTokenException(string message = "Invalid or expired token")
            : base("Invalid Token", StatusCodes.Status401Unauthorized, message) { }
    }

    public class InsufficientPrivilegeException : ApplicationException
    {
        public InsufficientPrivilegeException(string message = "Insufficient privileges to perform this action")
            : base("Forbidden", StatusCodes.Status403Forbidden, message) { }
    }
}
