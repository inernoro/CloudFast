using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Cloud.Framework.Mongo;

namespace Cloud.ApiManager.Manager
{
    [AutoMap(typeof(Domain.TestManager))]
    public class TestInput
    {
        [Required]
        public string Url { get; set; }

        public string Data { get; set; }

        [Required]
        public HttpReponse Type { get; set; }

        public string DateType { get; set; } = "JSON";

        public string ContentType { get; set; } = "application/json";

        public string Success { get; set; }

        public string Error { get; set; }

        public bool IsLogin { get; set; } = false;


    }
}