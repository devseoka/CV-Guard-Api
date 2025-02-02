namespace Cv.Guard.Api.Core.Dto
{
	public class UploadRequest
	{
		public string Initials { get; set; }
		public IFormFile File { get; set; }
	}
}
