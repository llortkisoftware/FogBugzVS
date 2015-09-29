using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogBugzAPI.Exceptions
{
    /// <summary>
    /// Base class for all exceptions for the FogBuzAPI library
    /// </summary>
    public class FogBugzException : Exception
    {
        public FogBugzException(string message) : base(message)
        {
        }

        public FogBugzException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
