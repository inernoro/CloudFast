using System;
using Abp.AutoMapper;
using Cloud.Temp;

namespace Cloud.UserInfoApp.Dtos
{
	[AutoMap(typeof(UserInfo))]
	public class PostInput {
		public string UserName{ get; set; }
		public string Password{ get; set; }
		public string Phone{ get; set; }
		public string Email{ get; set; }
		public string HeadPortrait{ get; set; }
		public int Role{ get; set; }
		public int Enable{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}