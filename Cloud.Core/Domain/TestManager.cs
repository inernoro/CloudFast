using System;
using Abp.Domain.Entities;
using Cloud.Framework.Mongo;

namespace Cloud.Domain
{
    public class TestManager : Entity<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();

        public string Data { get; set; }

        public HttpReponse Type { get; set; }

        public string DateType { get; set; }

        public string ContentType { get; set; }

        public string Success { get; set; }

        public string Error { get; set; }

        public bool IsLogin { get; set; }
    }
}