using System;

namespace HearstMappingsEditor.Common.Exceptions
{
    [Serializable]
    public class LogException : Exception
    {
        public LogException()
        {
        }

        public LogException(string message) : base(message)
        {
        }

        public LogException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
