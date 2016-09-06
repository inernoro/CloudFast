using Abp.AutoMapper;

namespace Cloud.Temp.Student.Dtos{
	[AutoMap(typeof(Domain.Student))]
	public class StudentDto{
		public string Name{ get; set; }
	}
}