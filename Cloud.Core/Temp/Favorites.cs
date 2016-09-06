using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class Favorites :Entity {
		public override int Id{ get; set; }
		public int UserId{ get; set; }
		public int Category{ get; set; }
		public int PrimaryKey{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}