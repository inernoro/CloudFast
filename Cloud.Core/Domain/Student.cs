using Abp.Domain.Entities;
namespace Cloud.Domain
{
    public class Student : Entity
    {
        public override int Id { get; set; }
        public string Name { get; set; }
    }
}