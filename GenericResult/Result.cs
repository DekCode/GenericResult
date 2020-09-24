using System.Collections.Generic;
using System.Linq;

namespace GenericResult
{
    public abstract class ResultBase<TValue> where TValue : ResultBase<TValue>
    {

    }

    /// <summary>
    /// Use this to create a result
    /// </summary>
    public partial class Result
    {
        /// <summary>
        /// Creates a successful result
        /// </summary>
        /// <returns></returns>
        public static Result Succeed()
        {
            return new Result(true);
        }

        /// <summary>
        /// Creates a failed result
        /// </summary>
        /// <returns></returns>
        public static Result Fail()
        {
            return new Result(false);
        }

        /// <summary>
        /// Creates an error result with a message
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Result Fail(string errorMessage)
        {
            return new Result(errorMessage);
        }

        /// <summary>
        /// Creates an error result with a list of messages
        /// </summary>
        /// <param name="errorMessages"></param>
        /// <returns></returns>
        public static Result Fail(List<string> errorMessages)
        {
            return new Result(errorMessages);
        }

        /// <summary>
        /// Creates a successful result with a value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Result<T> Succeed<T>(T value)
        {
            return new Result<T>()
                .Succeed(value);
        }

        /// <summary>
        /// Creates an error result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Result<T> Fail<T>()
        {
            return new Result<T>()
                .Fail((string)null);
        }

        /// <summary>
        /// Creates an error result with a message
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Result<T> Fail<T>(string errorMessage)
        {
            return new Result<T>()
                .Fail(errorMessage);
        }

        /// <summary>
        /// Creates an error result with a list of messages
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="errorMessages"></param>
        /// <returns></returns>
        public static Result<T> Fail<T>(List<string> errorMessages)
        {
            return new Result<T>()
                .Fail(errorMessages);
        }
    }

    /// <summary>
    /// Represents a result without a value
    /// </summary>
    public partial class Result : ResultBase<Result>
    {
        /// <summary>
        /// Indicates that the result has failed
        /// </summary>
        public bool IsFailed => !IsSuccessful;

        /// <summary>
        /// Indicates that the result has succeeded
        /// </summary>
        public bool IsSuccessful { get; private set; }

        /// <summary>
        /// Gets a list of error messagages from a failed <see cref="Result{T}"/>
        /// </summary>
        public List<string> Errors { get; private set; } = new List<string>();

        /// <summary>
        /// Gets an error messagages from a failed <see cref="Result{T}"/>
        /// This returns the first error message if there are multiple.
        /// </summary>
        public string Error => Errors.FirstOrDefault();

        public Result(bool sucessful)
        {
            IsSuccessful = sucessful;
        }

        public Result(string errorMessage)
        {
            Errors = new List<string> { errorMessage };
        }

        public Result(List<string> errorMessages)
        {
            Errors = errorMessages ?? new List<string>();
        }
    }

    /// <summary>
    /// Represents a result which contains a value on success or errors on failure
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T> : ResultBase<Result<T>>
    {
        /// <summary>
        /// Gets the value from a successful <see cref="Result{T}"/>
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Gets a list of error messagages from a failed <see cref="Result{T}"/>
        /// </summary>
        public List<string> Errors { get; private set; } = new List<string>();

        /// <summary>
        /// Gets an error messagages from a failed <see cref="Result{T}"/>
        /// This returns the first error message if there are multiple.
        /// </summary>
        public string Error => Errors.FirstOrDefault();

        /// <summary>
        /// Indicates that the result has failed
        /// </summary>
        public bool IsFailed => !IsSuccessful;

        /// <summary>
        /// Indicates that the result has succeeded
        /// </summary>
        public bool IsSuccessful { get; private set; }

        public Result<T> Succeed(T value)
        {
            Value = value;
            IsSuccessful = true;
            return this;
        }

        public Result<T> Fail(string errorMessage)
        {
            Errors = new List<string> { errorMessage };
            return this;
        }

        public Result<T> Fail(List<string> errorMessages)
        {
            Errors = errorMessages ?? new List<string>();
            return this;
        }
    }
}
