namespace Cv.Guard.Api.Core.Models
{
	public class Upload : DataModelBase
	{
		public string Name { get; set; }
		public string MimeType { get; set; }
		public string Path { get; set; }
		public string Extension { get; set; }
		public string Size { get; set; }
	}
}
