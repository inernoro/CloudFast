using System;
using Abp.AutoMapper;
namespace Cloud.Student.Dtos{
	[AutoMap(typeof(Domain.Student))]
	public class StudentDto{
		public string Name{ get; set; }
	}
}