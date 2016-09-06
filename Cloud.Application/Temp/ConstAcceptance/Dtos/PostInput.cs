using System;
using Abp.AutoMapper;
namespace Cloud.ConstAcceptance.Dtos
{
	[AutoMap(typeof(Domain.ConstAcceptance))]
	public class PostInput {
		public int ConstId{ get; set; }
		public int State{ get; set; }
		public int Role{ get; set; }
		public int Progress{ get; set; }
	}
}