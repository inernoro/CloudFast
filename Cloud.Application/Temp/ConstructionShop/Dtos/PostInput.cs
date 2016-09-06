using System;
using Abp.AutoMapper;

namespace Cloud.Temp.ConstructionShop.Dtos
{
	[AutoMap(typeof(Domain.ConstructionShop))]
	public class PostInput {
		public int ConstId{ get; set; }
		public int ShopId{ get; set; }
		public int ProprietorScore{ get; set; }
		public int Credits{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}