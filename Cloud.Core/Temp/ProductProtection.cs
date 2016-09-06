using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class ProductProtection :Entity {
		public override int Id{ get; set; }
		public int ConstId{ get; set; }
		public int Score{ get; set; }
		public int State{ get; set; }
		public string Evaluation{ get; set; }
		public DateTime CreateTime{ get; set; }
		public string Image{ get; set; }
	}
}