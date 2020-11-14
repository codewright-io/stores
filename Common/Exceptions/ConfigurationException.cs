using System;

namespace DevKnack.Common.Exceptions
{
    /// <summary>
    /// Bad configuration
    /// </summary>
    public class ConfigurationException : ApplicationException
    {
        public ConfigurationException()
        {
        }

        public ConfigurationException(string message) : base(message)
        {
        }

        public ConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
