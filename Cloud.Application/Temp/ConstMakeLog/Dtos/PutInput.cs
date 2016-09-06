using Abp.AutoMapper;
namespace Cloud.ConstMakeLog.Dtos{
[AutoMap(typeof(Domain.ConstMakeLog))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}