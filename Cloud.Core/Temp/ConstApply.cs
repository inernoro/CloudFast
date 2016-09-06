using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class ConstApply :Entity {
		public override int Id{ get; set; }
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