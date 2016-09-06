using Abp.Domain.Entities;
using System;
namespace Cloud.Domain{
	public class LogoType :Entity {
		public int id{ get; set; }
		public string name{ get; set; }
		public DateTime cratetime{ get; set; }
	}
}