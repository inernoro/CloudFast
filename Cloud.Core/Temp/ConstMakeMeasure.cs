using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class ConstMakeMeasure :Entity {
		public override int Id{ get; set; }
		public int ConstMakeId{ get; set; }
		public int ProcedureId{ get; set; }
		public double MeasureRecord{ get; set; }
		public int State{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}