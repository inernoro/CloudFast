using System;
using Abp.AutoMapper;
namespace Cloud.ConstMakeMeasure.Dtos
{
	[AutoMap(typeof(Domain.ConstMakeMeasure))]
	public class PostInput {
		public int ConstMakeId{ get; set; }
		public int ProcedureId{ get; set; }
		public double MeasureRecord{ get; set; }
		public int State{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}