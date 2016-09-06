using System;
using Abp.AutoMapper;
namespace Cloud.ConstApply.Dtos
{
	[AutoMap(typeof(Domain.ConstApply))]
	public class PostInput {
		public int CityId{ get; set; }
		public int PrimaryKey{ get; set; }
		public string PersonName{ get; set; }
		public string Phone{ get; set; }
		public double Size{ get; set; }
		public string Description{ get; set; }
		public int State{ get; set; }
		public string ReturnDescription{ get; set; }
		public int Category{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}