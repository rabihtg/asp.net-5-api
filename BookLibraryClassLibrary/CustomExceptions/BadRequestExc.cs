using System;
using System.Net;

namespace BookLibraryClassLibrary.CustomExceptions
{
    public class BadRequestExc : CustomExceptionsBase
    {
        public override int StatusCode { get; init; } = ((int)HttpStatusCode.BadRequest);
        public BadRequestExc() : base() { }

        public BadRequestExc(string message, int statusCode) : base(message, statusCode)
        {
            StatusCode = statusCode;
        }

        public BadRequestExc(string message, Exception innerException, int statusCode) : base(message, innerException, statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
