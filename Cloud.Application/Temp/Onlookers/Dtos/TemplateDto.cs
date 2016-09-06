using System;
using Abp.AutoMapper;

namespace Cloud.Temp.Onlookers.Dtos{
	[AutoMap(typeof(Domain.Onlookers))]
	public class OnlookersDto{
		public int UserId{ get; set; }
		public int ConstId{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}