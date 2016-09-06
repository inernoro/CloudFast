using System;
using Abp.AutoMapper;
namespace Cloud.Bid.Dtos
{
	[AutoMap(typeof(Domain.Bid))]
	public class PostInput {
		public int ShopId{ get; set; }
		public int TenderId{ get; set; }
		public int State{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}