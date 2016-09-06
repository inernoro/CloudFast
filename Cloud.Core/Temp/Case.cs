using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class Case :Entity {
		public override int Id{ get; set; }
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