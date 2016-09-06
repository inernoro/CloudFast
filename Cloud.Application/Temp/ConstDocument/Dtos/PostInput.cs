using System;
using Abp.AutoMapper;

namespace Cloud.Temp.ConstDocument.Dtos
{
	[AutoMap(typeof(Domain.ConstDocument))]
	public class PostInput {
		public int ConstId{ get; set; }
		public int Category{ get; set; }
		public string Title{ get; set; }
		public string Url{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}