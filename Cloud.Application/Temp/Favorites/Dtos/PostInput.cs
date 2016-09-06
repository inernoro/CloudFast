using System;
using Abp.AutoMapper;
namespace Cloud.Favorites.Dtos
{
	[AutoMap(typeof(Domain.Favorites))]
	public class PostInput {
		public int UserId{ get; set; }
		public int Category{ get; set; }
		public int PrimaryKey{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}