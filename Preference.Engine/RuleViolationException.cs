// 18/01/2012 by Dmitry Vorobyev.

using System;
using System.Runtime.Serialization;

namespace Preference.Engine
{
    public class RuleViolationException : Exception
    {
        public RuleViolationException()
        {
        }

        public RuleViolationException(string message) : base(message)
        {
        }

        public RuleViolationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RuleViolationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
