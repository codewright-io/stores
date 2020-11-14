using System;

namespace DevKnack.Common.Exceptions
{
    /// <summary>
    /// Received an invalid command (HTTP 400)
    /// </summary>
    public class InvalidCommandException : ApplicationException
    {
        public InvalidCommandException() : base()
        {
        }

        public InvalidCommandException(string message) : base(message)
        {
        }

        public InvalidCommandException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}