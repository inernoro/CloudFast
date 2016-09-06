using System;
using Abp.AutoMapper;

namespace Cloud.Temp.Case.Dtos
{
	[AutoMap(typeof(Domain.Case))]
	public class PostInput {
		public int UserId{ get; set; }
		public int HouseType{ get; set; }
		public int ConstId{ get; set; }
		public string Name{ get; set; }
		public string Description{ get; set; }
		public string MainImg{ get; set; }
		public string ImgList{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}