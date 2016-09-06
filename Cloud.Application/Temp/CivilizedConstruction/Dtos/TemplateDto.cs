using System;
using Abp.AutoMapper;
namespace Cloud.CivilizedConstruction.Dtos{
	[AutoMap(typeof(Domain.CivilizedConstruction))]
	public class CivilizedConstructionDto{
		public int ConstId{ get; set; }
		public int Score{ get; set; }
		public int State{ get; set; }
		public string Evaluation{ get; set; }
		public DateTime CreateTime{ get; set; }
		public string Image{ get; set; }
	}
}