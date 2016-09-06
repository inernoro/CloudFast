using Abp.AutoMapper;
namespace Cloud.ConstMemberRelation.Dtos{
[AutoMap(typeof(Domain.ConstMemberRelation))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}