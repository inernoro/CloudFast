using Abp.AutoMapper;

namespace Cloud.Temp.ManagerNavigation.Dtos{
[AutoMap(typeof(Domain.ManagerNavigation))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}