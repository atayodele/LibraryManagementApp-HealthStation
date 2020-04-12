using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryMgtApp.Extensions
{
    public class ApiException : Exception
    {
        readonly ILogger _logger;

        public int StatusCode { get; set; }

        public List<ValidationError> Errors { get; set; }

        public ApiException(string message, ILogger logger, int statusCode = 500, List<ValidationError> errors = null) :
            base(message)
        {
            this._logger = logger;
            this._logger.LogError(errors?.Join(" +++ "));
            StatusCode = statusCode;
            Errors = errors;
        }
        public ApiException(Exception ex, ILogger logger, int statusCode = 500) : base(ex.Message)
        {
            this._logger = logger;
            StatusCode = statusCode;
            this._logger.LogError(ex, ex.Message);
        }
    }
}
