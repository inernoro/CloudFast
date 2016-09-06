using System;
using Abp.AutoMapper;
namespace Cloud.ProductProtection.Dtos
{
	[AutoMap(typeof(Domain.ProductProtection))]
	public class PostInput {
		public int ConstId{ get; set; }
		public int Score{ get; set; }
		public int State{ get; set; }
		public string Evaluation{ get; set; }
		public DateTime CreateTime{ get; set; }
		public string Image{ get; set; }
	}
}