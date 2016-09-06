using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class City :Entity {
		public override int Id{ get; set; }
		public string CityName{ get; set; }
		public int ParentId{ get; set; }
	}
}