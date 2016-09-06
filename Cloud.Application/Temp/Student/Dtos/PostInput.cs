using Abp.AutoMapper;

namespace Cloud.Temp.Student.Dtos
{
	[AutoMap(typeof(Domain.Student))]
	public class PostInput {
		public string Name{ get; set; }
	}
}