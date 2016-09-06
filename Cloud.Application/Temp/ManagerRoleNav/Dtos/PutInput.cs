using Abp.AutoMapper;

namespace Cloud.Temp.ManagerRoleNav.Dtos{
[AutoMap(typeof(Domain.ManagerRoleNav))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}