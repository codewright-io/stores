using System;

namespace DevKnack.Common.Exceptions
{
    /// <summary>
    /// Throw when an object or file is not found (HTTP 404)
    /// </summary>
    public class NotFoundException : ApplicationException
    {
        public NotFoundException() : base()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, string identifier) : base($"{message} : {identifier}")
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
