using System;
using Abp.AutoMapper;
namespace Cloud.Test.Dtos
{
	[AutoMap(typeof(Domain.Test))]
	public class PostInput {
		public string Name{ get; set; }
	}
}