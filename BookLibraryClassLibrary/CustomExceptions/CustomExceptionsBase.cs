using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.CustomExceptions
{
    public abstract class CustomExceptionsBase : Exception
    {
        public abstract int StatusCode { get; init; }

        protected CustomExceptionsBase() : base() { }
        protected CustomExceptionsBase(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
        protected CustomExceptionsBase(string message, Exception innerException, int statusCode) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
