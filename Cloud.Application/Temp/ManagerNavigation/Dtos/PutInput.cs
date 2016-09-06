using Abp.AutoMapper;
namespace Cloud.ManagerNavigation.Dtos{
[AutoMap(typeof(Domain.ManagerNavigation))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}