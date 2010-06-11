using System;
using System.Runtime.Serialization;

namespace Bin4Net.Publishing.PublishCommands
{
    [Serializable]
    public class CommandExecutionException : Exception
    {

        public CommandExecutionException()
        {
        }

        public CommandExecutionException(string message) : base(message)
        {
        }

        public CommandExecutionException(string message, Exception inner) : base(message, inner)
        {
        }

        protected CommandExecutionException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}