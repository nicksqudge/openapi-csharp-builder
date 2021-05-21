using System;

namespace OpenApiBuilder.Tests.TestUtilities.ExampleClasses
{
    public class Result
    {
        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public string Error { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsFailure => IsSuccess == false;

        public static Result Success()
            => new Result(true);

        public static Result Fail(string error)
            => new Result(false)
            {
                Error = error
            };

        public static Result<TValue> Success<TValue>(TValue value)
            => new Result<TValue>(true, value);

        public static Result<TValue> Fail<TValue>(TValue value)
            => new Result<TValue>(false, value);

        public static Result<TValue> Fail<TValue>(string error)
            => new Result<TValue>(false, default(TValue))
            {
                Error = error
            };
    }
    
    public class Result<TValue> : Result
    {
        public Result(bool isSuccess, TValue value) : base(isSuccess)
        {
            Value = value;
        }

        
        public TValue Value { get; set; }
    }
}
