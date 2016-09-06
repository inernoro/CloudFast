using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class sysdiagrams :Entity {
		public string name{ get; set; }
		public int principal_id{ get; set; }
		public int diagram_id{ get; set; }
		public int version{ get; set; }
		public string definition{ get; set; }
	}
}