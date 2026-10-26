using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ECommerce.Application.Common
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public IReadOnlyList<Error> Errors { get; }

        public Result(bool isSuccess, IReadOnlyList<Error> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }
        public static Result Ok() => new(true, Array.Empty<Error>());
        public static Result Fail(Error error) => new(false, new[] { error });
        public static Result Fail(IReadOnlyList<Error> errors) => new(false, errors);

    }

    public class Result<TValue> : Result
    {


        public TValue Data { get; set; }

        
        public Result(TValue value) : base(true, Array.Empty<Error>())
        {
            Data = value;
        }
        public Result(Error error) : base(false, new[] { error })
        {
        }
        public Result(IReadOnlyList<Error> errors) : base(false, errors)
        {
        }

        public static Result<TValue> Ok(TValue value) => new Result<TValue>(value);
        public static Result<TValue> Fail(Error error) => new Result<TValue>(error);
        public static Result<TValue> Fail(IReadOnlyList<Error> errors) => new Result<TValue>(errors);

        public static implicit operator Result<TValue>(TValue value) => Ok(value);
        public static implicit operator Result<TValue>(Error error) => Fail(error);
    }

}