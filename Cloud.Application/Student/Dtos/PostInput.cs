using Abp.AutoMapper;
namespace Cloud.Student.Dtos
{
	[AutoMap(typeof(Domain.Student))]
	public class PostInput {
		public string Name{ get; set; }
	}
}