using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Results
{
    public struct Result<TResult>
    {
        public TResult Value { get; private set; }
        public Error Error { get; private set; }
        public string? Message { get; private set; }
        public bool IsSucceess { get; private set; }

        private Result(TResult value, string message)
        {
            Value = value;
            Error = Error.Success;
            Message = message;
            IsSucceess = true;
        }

        private Result(Error error, string message)
        {
            Value = default;
            Error = error;
            Message = message;
            IsSucceess = false;

        }

        /// <summary>
        /// Create success result
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Result<TResult> CreateSuccess(TResult value, string message = null)
        {
            return new Result<TResult>(value, message);
        }


        /// <summary>
        /// Create failure result
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static Result<TResult> CreateFailure(Error error, string message)
        {
            return new Result<TResult>(error, message);
        }

        public static Result<TResult> CreateFailure<T>(Result<T> baseResult)
        {
            return new Result<TResult>(baseResult.Error, baseResult.Message);
        }
    }
}
