using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class Knowledge :Entity {
		public override int Id{ get; set; }
		public int Category{ get; set; }
		public string Title{ get; set; }
		public string Alt{ get; set; }
		public string CategoryName{ get; set; }
		public string Details{ get; set; }
		public string Url{ get; set; }
		public int Creator{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}