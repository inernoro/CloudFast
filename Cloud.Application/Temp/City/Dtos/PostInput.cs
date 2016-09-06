using System;
using Abp.AutoMapper;
namespace Cloud.City.Dtos
{
	[AutoMap(typeof(Domain.City))]
	public class PostInput {
		public string CityName{ get; set; }
		public int ParentId{ get; set; }
	}
}