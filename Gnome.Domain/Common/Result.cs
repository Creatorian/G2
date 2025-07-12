using System;
using System.Collections.Generic;

namespace Gnome.Domain.Common
{
    /// <summary>
    /// Represents the result of an operation that can either succeed or fail
    /// </summary>
    /// <typeparam name="T">The type of the success value</typeparam>
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }
        public string Error { get; }
        public List<string> ValidationErrors { get; }

        private Result(bool isSuccess, T value = default, string error = null, List<string> validationErrors = null)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
            ValidationErrors = validationErrors ?? new List<string>();
        }

        /// <summary>
        /// Creates a successful result
        /// </summary>
        public static Result<T> Success(T value) => new Result<T>(true, value);

        /// <summary>
        /// Creates a failed result
        /// </summary>
        public static Result<T> Failure(string error) => new Result<T>(false, error: error);

        /// <summary>
        /// Creates a failed result with validation errors
        /// </summary>
        public static Result<T> ValidationFailure(List<string> validationErrors) => 
            new Result<T>(false, validationErrors: validationErrors);

        /// <summary>
        /// Creates a failed result with validation errors
        /// </summary>
        public static Result<T> ValidationFailure(string validationError) => 
            new Result<T>(false, validationErrors: new List<string> { validationError });

        /// <summary>
        /// Implicit conversion from T to Result<T>
        /// </summary>
        public static implicit operator Result<T>(T value) => Success(value);
    }

    /// <summary>
    /// Represents the result of an operation that can either succeed or fail (without a value)
    /// </summary>
    public class Result
    {
        public bool IsSuccess { get; }
        public string Error { get; }
        public List<string> ValidationErrors { get; }

        private Result(bool isSuccess, string error = null, List<string> validationErrors = null)
        {
            IsSuccess = isSuccess;
            Error = error;
            ValidationErrors = validationErrors ?? new List<string>();
        }

        /// <summary>
        /// Creates a successful result
        /// </summary>
        public static Result Success() => new Result(true);

        /// <summary>
        /// Creates a failed result
        /// </summary>
        public static Result Failure(string error) => new Result(false, error);

        /// <summary>
        /// Creates a failed result with validation errors
        /// </summary>
        public static Result ValidationFailure(List<string> validationErrors) => 
            new Result(false, validationErrors: validationErrors);

        /// <summary>
        /// Creates a failed result with validation errors
        /// </summary>
        public static Result ValidationFailure(string validationError) => 
            new Result(false, validationErrors: new List<string> { validationError });
    }
} 