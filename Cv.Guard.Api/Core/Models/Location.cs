namespace Cv.Guard.Api.Core.Models
{
	public class Location : DataModelBase
	{
		public string Ip { get; set; }
		public string Host { get; set; }
		public string Latitude { get; set; }
		public string Longitude { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public string Country { get; set; }
		public string Zip { get; set; }
	}
}
