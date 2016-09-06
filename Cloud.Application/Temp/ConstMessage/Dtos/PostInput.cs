using System;
using Abp.AutoMapper;
namespace Cloud.ConstMessage.Dtos
{
	[AutoMap(typeof(Domain.ConstMessage))]
	public class PostInput {
		public int Category{ get; set; }
		public string Description{ get; set; }
		public int Role{ get; set; }
		public int ShopId{ get; set; }
		public int UserId{ get; set; }
		public int ConstId{ get; set; }
		public int ConstMakeId{ get; set; }
		public int ReadState{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}