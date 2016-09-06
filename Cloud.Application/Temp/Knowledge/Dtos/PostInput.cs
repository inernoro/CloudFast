using System;
using Abp.AutoMapper;

namespace Cloud.Temp.Knowledge.Dtos
{
	[AutoMap(typeof(Domain.Knowledge))]
	public class PostInput {
		public int Category{ get; set; }
		public string Title{ get; set; }
		public string Alt{ get; set; }
		public string CategoryName{ get; set; }
		public string Details{ get; set; }
		public string Url{ get; set; }
		public int Creator{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}