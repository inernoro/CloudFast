using System.ComponentModel.DataAnnotations;
using Cloud.Framework.Mongo;

namespace Cloud.ApiManager.Manager
{
    public class TestInput
    {
        [Required]
        public string Url { get; set; }


        public string Data { get; set; }

        [Required]
        public HttpReponse Type { get; set; }

        public string DateType { get; set; }

        public string ContentType { get; set; }

        public string Success { get; set; }

        public string Error { get; set; }

        public bool IsLogin { get; set; }
         
    }
}