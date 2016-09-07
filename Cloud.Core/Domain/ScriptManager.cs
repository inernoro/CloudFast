using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class ScriptManager :Entity {
		public override int Id{ get; set; }
		public string Body{ get; set; }
		public string Parament{ get; set; }
		public string Result{ get; set; }
		public string Name{ get; set; }
		public string Url{ get; set; }
	}
}