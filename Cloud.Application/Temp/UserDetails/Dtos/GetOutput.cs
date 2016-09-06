using System;
namespace Cloud.UserDetails.Dtos {
public class GetOutput {
  
		public int UserId{ get; set; }
		public string Name{ get; set; }
		public int Sex{ get; set; }
		public int Age{ get; set; }
		public int CityId{ get; set; }
		public string Character{ get; set; }
		public string IdCard{ get; set; }
		public string IdCardPositive{ get; set; }
		public string IdCardBoth{ get; set; }
		public string HealthyState{ get; set; }
		public string JobExperience{ get; set; }
		public string DesignerStyle{ get; set; }
		public string CertificateName{ get; set; }
		public string CertificateImg{ get; set; }
		public string Hometown{ get; set; }
		public string Company{ get; set; }
		public string School{ get; set; }
		public int Seniority{ get; set; }
		public string Profile{ get; set; }
		public int Recommend{ get; set; }
		public int Star{ get; set; }
		public int Credits{ get; set; }
		public int Complete{ get; set; }
		public int NotComplete{ get; set; }
		public int QualifiedRate{ get; set; }
		public int AverageScore{ get; set; }
		public string Address{ get; set; }  
	}
}