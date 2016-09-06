using System;
using Abp.AutoMapper;

namespace Cloud.Temp.ConstMake.Dtos{
	[AutoMap(typeof(Domain.ConstMake))]
	public class ConstMakeDto{
		public int ConstId{ get; set; }
		public int ConstProcessId{ get; set; }
		public int ApplyTimes{ get; set; }
		public int Score{ get; set; }
		public string SuccessDescription{ get; set; }
		public string ImgUrl{ get; set; }
		public int State{ get; set; }
		public string Evaluation{ get; set; }
		public DateTime PlanStart{ get; set; }
		public DateTime PlanEnd{ get; set; }
		public DateTime ActualStart{ get; set; }
		public DateTime ActualEnd{ get; set; }
		public DateTime ApplyTime{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}