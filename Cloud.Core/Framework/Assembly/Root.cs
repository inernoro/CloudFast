using System.Activities.Validation;
using System.Collections.Generic;

namespace Cloud.Framework.Assembly
{ 
    public class Error
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 您的请求无效!
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string details { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ValidationError> validationErrors { get; set; }
    }
     

    public class Root<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public T result { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Error error { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string unAuthorizedRequest { get; set; }
    }
}