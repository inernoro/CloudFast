using System;

namespace Cloud.Temp.ConstMakeMeasure.Dtos {
public class GetOutput {
  
		public int ConstMakeId{ get; set; }
		public int ProcedureId{ get; set; }
		public double MeasureRecord{ get; set; }
		public int State{ get; set; }
		public DateTime CreateTime{ get; set; }  
	}
}