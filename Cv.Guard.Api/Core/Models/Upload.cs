namespace Cv.Guard.Api.Core.Models
{
	public class Upload : DataModelBase
	{
		public string Name { get; set; }
		public string MimeType { get; set; }
		public string Path { get; set; }
		public string Extension { get; set; }
		public double Size { get; set; }
		public string Key { get; set; }
	}
}
