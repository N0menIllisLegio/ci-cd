using System;
using System.Net;

namespace ci_cd.Exceptions
{
  internal class AppHttpException : Exception
  {
    public AppHttpException(HttpStatusCode statusCode, string message)
      : base(message)
    {
      StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; set; }
  }
}
