using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioAPI.DataServices.DataAccessors.Class
{
    /// <summary>
    /// Status code for data interactions
    /// </summary>
    public enum DataStatusCode
    {
        /// <summary>
        /// Data interaction succeeded
        /// </summary>
        SUCCESS,
        /// <summary>
        /// An exception occurred while performing data interaction
        /// </summary>
        ERROR,
        /// <summary>
        /// The data interaction request was invalid
        /// </summary>
        INVALID
    };

    /// <summary>
    /// Class for wrapping values returned by data accessors
    /// </summary>
    /// <typeparam name="T">The type of the wrapped return value</typeparam>
    public class DataResponse<T> : DataResponse
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="status">The status of the data interaction</param>
        /// <param name="message">The message associated with this data interaction</param>
        public DataResponse(DataStatusCode status, string message) : base(status, message) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="status">The status of the data interaction</param>
        public DataResponse(DataStatusCode status) : base(status) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="returnVal">The wrapped return value</param>
        /// <param name="status">The status of the data interaction</param>
        public DataResponse(T returnVal, DataStatusCode status) : base(status)
        {
            Value = returnVal;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="returnVal">The wrapped return value</param>
        /// <param name="status">The status of the data interaction</param>
        /// <param name="message">A message associated with the result of this data interaction</param>
        public DataResponse(T returnVal, DataStatusCode status, string message) : base(status, message)
        {
            Value = returnVal;
        }

        /// <summary>
        /// The wrapped return value
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Whether or not this response has a return value
        /// </summary>
        public bool HasValue
        {
            get
            {
                return Value != null;
            }
        }
    }

    /// <summary>
    /// Class for indicating the result of a data interaction
    /// </summary>
    public class DataResponse
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="status">The status of the data interaction</param>
        /// <param name="message">The message associated with this data interaction</param>
        public DataResponse(DataStatusCode status, string message)
        {
            Status = status;
            Message = message;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="status">The status of the data interaction</param>
        public DataResponse(DataStatusCode status)
        {
            Status = status;
        }

        /// <summary>
        /// The status of the data interaction
        /// </summary>
        public DataStatusCode Status { get; set; }

        /// <summary>
        /// The message associated with this data interaction
        /// </summary>
        public string Message { get; set; } = "";
    }
}