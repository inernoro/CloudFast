using System;
namespace Cloud.Tender.Dtos {
public class GetOutput {
  
		public int UserId{ get; set; }
		public string Phone{ get; set; }
		public string ProprietorName{ get; set; }
		public int CityId{ get; set; }
		public int TenderType{ get; set; }
		public double BuiltArea{ get; set; }
		public int HouseType{ get; set; }
		public string Uptown{ get; set; }
		public int Budget{ get; set; }
		public int DecorateType{ get; set; }
		public string Description{ get; set; }
		public int RegionLimit{ get; set; }
		public int StarLimit{ get; set; }
		public int QuantityLimit{ get; set; }
		public int TimeLimit{ get; set; }
		public int FinishState{ get; set; }
		public DateTime CreateTime{ get; set; }
		public DateTime FinishTime{ get; set; }  
	}
}