using System;
using Abp.AutoMapper;
namespace Cloud.ConstMakeQuestion.Dtos{
	[AutoMap(typeof(Domain.ConstMakeQuestion))]
	public class ConstMakeQuestionDto{
		public int id{ get; set; }
		public int ConstMakeId{ get; set; }
		public int Space{ get; set; }
		public string ImageUrl{ get; set; }
		public string Description{ get; set; }
		public string ChangedImageUrl{ get; set; }
		public string ChangedDescription{ get; set; }
		public int State{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}