using System.Collections.Generic;
using Ila.NLayer.ProjectTemplates.Core.Enums;

namespace Ila.NLayer.ProjectTemplates.Core.Models.Results
{
    public class Result<T>
    {
        public Result()
        {
        }

        public Result(T value)
        {
            Data = value;
        }

        private Result(ResultStatus status)
        {
            Status = status;
        }

        public static implicit operator T(Result<T> result) => result.Data;

        public static implicit operator Result<T>(T value) => Success(value);

        public T Data { get; set; } 

        public bool HasInValid => Status == ResultStatus.Invalid;

        public ResultStatus Status { get; set; } = ResultStatus.Ok;

        public bool IsSuccess => Status == ResultStatus.Ok;

        public string SuccessMessage { get; set; } = string.Empty;

        public IEnumerable<string> Errors { get; set; } = new List<string>();

        public List<ValidationError> ValidationErrors { get; set; } = new List<ValidationError>();

        public static Result<T> Success(T value)
        {
            return new Result<T>(value);
        }

        public static Result<T> Success(T value, string successMessage)
        {
            return new Result<T>(value) { SuccessMessage = successMessage };
        }

        public static Result<T> Error(params string[] errorMessages)
        {
            return new Result<T>(ResultStatus.Error) { Errors = errorMessages };
        }

        public static Result<T> Invalid(List<ValidationError> validationErrors)
        {
            return new Result<T>(ResultStatus.Invalid) { ValidationErrors = validationErrors };
        }

        public static Result<T> Invalid(string errorMessage, string identifier)
        {
            return Result<T>.Invalid(new List<ValidationError>
                {
                    new ValidationError
                    {
                        Identifier = identifier,
                         ErrorMessage = errorMessage
                    }
                });
        }

        public static Result<T> NotFound()
        {
            return new Result<T>(ResultStatus.NotFound);
        }

        public static Result<T> Forbidden()
        {
            return new Result<T>(ResultStatus.Forbidden);
        }

        public static Result<T> Unauthorized()
        {
            return new Result<T>(ResultStatus.Unauthorized);
        }
    }
}

