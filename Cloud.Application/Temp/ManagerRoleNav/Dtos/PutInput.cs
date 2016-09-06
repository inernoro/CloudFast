using Abp.AutoMapper;
namespace Cloud.ManagerRoleNav.Dtos{
[AutoMap(typeof(Domain.ManagerRoleNav))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}