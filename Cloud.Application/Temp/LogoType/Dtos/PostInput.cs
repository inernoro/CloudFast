using System;
using Abp.AutoMapper;
namespace Cloud.LogoType.Dtos
{
	[AutoMap(typeof(Domain.LogoType))]
	public class PostInput {
		public int id{ get; set; }
		public string name{ get; set; }
		public DateTime cratetime{ get; set; }
	}
}