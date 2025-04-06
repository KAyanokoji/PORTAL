using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTAL.DOMAIN.Exceptions
{
    public class UserNotFoundException : ApplicationException
    {
        public UserNotFoundException(string userId)
            : base("User Not Found", StatusCodes.Status404NotFound, $"User with ID {userId} not found") { }
    }

    public class UsernameExistsException : ApplicationException
    {

        public UsernameExistsException(string username)
            : base("Username Exists", StatusCodes.Status409Conflict,
                  $"Username '{username}' is already taken")
        { }
    }

    public class EmailExistsException : ApplicationException
    {
        public EmailExistsException(string email)
            : base("Email Exists", StatusCodes.Status409Conflict,
                  $"Email '{email}' is already registered")
        { }
    }

    public class InvalidPasswordException : ApplicationException
    {
        public InvalidPasswordException(string message = "Password does not meet requirements")
            : base("Invalid Password", StatusCodes.Status400BadRequest, message) { }
    }
}
