using Abp.AutoMapper;

namespace Cloud.Temp.ConstMakeQuestion.Dtos{
[AutoMap(typeof(Domain.ConstMakeQuestion))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}