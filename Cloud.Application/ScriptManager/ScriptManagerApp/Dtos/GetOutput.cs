using Abp.AutoMapper;

namespace Cloud.ScriptManager.ScriptManagerApp.Dtos {
	[AutoMap(typeof(Domain.ScriptManager))]
	
public class GetOutput {
  
		public string Body{ get; set; }
		public string Parament{ get; set; }
		public string Result{ get; set; }
		public string Name{ get; set; }
		public string Url{ get; set; }  
	}
}