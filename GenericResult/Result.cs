using System.Collections.Generic;
using System.Linq;

namespace GenericResult
{
    /// <summary>
    /// A result which contains a value on success or errors on failure
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T> where T : class, new()
    {
        private bool? _ExplicitSucessfulValue;

        /// <summary>
        /// Create a result explicitly
        /// </summary>
        /// <param name="failed"></param>
        public Result(bool? successful = null)
        {
            _ExplicitSucessfulValue = successful;
        }

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
        public bool IsFailed
        {
            get
            {
                return _ExplicitSucessfulValue.HasValue
                    ? !_ExplicitSucessfulValue.Value
                    : (Errors != null && Errors.Any());
            }
        }

        /// <summary>
        /// Indicates that the result has succeeded
        /// </summary>
        public bool IsSuccessful
        {
            get
            {
                return _ExplicitSucessfulValue ?? (Errors == null || !Errors.Any());
            }
        }

        /// <summary>
        /// Creates a successful result with a value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Result<T> Succeeds(T value)
        {
            return new Result<T>
            {
                Value = value
            };
        }

        /// <summary>
        /// Creates a successful result with a value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Result<T> Succeeds()
        {
            return new Result<T>(successful: true);
        }

        /// <summary>
        /// Creates an error result
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Result<T> Fails()
        {
            return new Result<T>(successful: false);
        }

        /// <summary>
        /// Creates an error result with a list of messages
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public static Result<T> Fails(List<string> messages)
        {
            return new Result<T>
            {
                Errors = messages ?? new List<string>()
            };
        }

        /// <summary>
        /// Creates an error result with a message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Result<T> Fails(string message)
        {
            return new Result<T>
            {
                Errors = new List<string>()
                {
                    message
                }
            };
        }
    }
}
