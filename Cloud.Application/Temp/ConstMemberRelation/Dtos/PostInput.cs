using System;
using Abp.AutoMapper;

namespace Cloud.Temp.ConstMemberRelation.Dtos
{
	[AutoMap(typeof(Domain.ConstMemberRelation))]
	public class PostInput {
		public int ConstId{ get; set; }
		public int UserId{ get; set; }
		public DateTime CreateTime{ get; set; }
		public int Credits{ get; set; }
		public int ProprietorScore{ get; set; }
	}
}