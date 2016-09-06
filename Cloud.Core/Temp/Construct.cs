using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class Construct :Entity {
		public override int Id{ get; set; }
		public string Name{ get; set; }
		public string Demand{ get; set; }
		public string HouseTypeDescribe{ get; set; }
		public string Address{ get; set; }
		public int UserId{ get; set; }
		public int FinishState{ get; set; }
		public DateTime PlanstartTime{ get; set; }
		public DateTime PlanendTime{ get; set; }
		public DateTime CreateTime{ get; set; }
		public int ConstProtection{ get; set; }
		public int ConstSafety{ get; set; }
	}
}