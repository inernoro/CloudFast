using System.Collections.Generic;

namespace Cloud.Manager.Models
{


    public class Error
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string details { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string validationErrors { get; set; }
    }
    public class ListResult<T>
    {
        public IList<T> items { get; set; }
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