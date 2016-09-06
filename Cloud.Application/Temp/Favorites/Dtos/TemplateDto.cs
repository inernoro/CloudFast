using System;
using Abp.AutoMapper;

namespace Cloud.Temp.Favorites.Dtos{
	[AutoMap(typeof(Domain.Favorites))]
	public class FavoritesDto{
		public int UserId{ get; set; }
		public int Category{ get; set; }
		public int PrimaryKey{ get; set; }
		public DateTime CreateTime{ get; set; }
	}
}