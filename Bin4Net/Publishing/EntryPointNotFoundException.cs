using System;
using System.Runtime.Serialization;

namespace Bin4Net.Publishing
{

    [Serializable]
    public class EntryPointNotFoundException : Exception
    {

        public EntryPointNotFoundException(string message) : base(message)
        {
        }

        public EntryPointNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected EntryPointNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}