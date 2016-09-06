namespace Cloud.Temp.ManagerNavigation.Dtos {
public class GetOutput {
  
		public string Navtype{ get; set; }
		public string Name{ get; set; }
		public string Title{ get; set; }
		public string Subtitle{ get; set; }
		public string Linkurl{ get; set; }
		public int Sortid{ get; set; }
		public int IsLock{ get; set; }
		public string Remark{ get; set; }
		public int ParentId{ get; set; }
		public string ClassList{ get; set; }
		public int ClassLayer{ get; set; }
		public int ChannelId{ get; set; }
		public string ActionType{ get; set; }
		public int Issys{ get; set; }  
	}
}