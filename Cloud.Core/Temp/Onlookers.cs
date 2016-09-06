using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class Onlookers :Entity {
		public override int Id{ get; set; }
		public int UserId{ get; set; }
		public int ConstId{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}