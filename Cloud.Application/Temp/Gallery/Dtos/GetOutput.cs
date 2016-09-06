using System;

namespace Cloud.Temp.Gallery.Dtos {
public class GetOutput {
  
		public int UserId{ get; set; }
		public int Style{ get; set; }
		public int Space{ get; set; }
		public int HouseType{ get; set; }
		public string Name{ get; set; }
		public string MainImg{ get; set; }
		public string CarouselFigure{ get; set; }
		public string Description{ get; set; }
		public string Feature{ get; set; }
		public string ImgList{ get; set; }
		public int Popularity{ get; set; }
		public DateTime CreateTime{ get; set; }  
	}
}