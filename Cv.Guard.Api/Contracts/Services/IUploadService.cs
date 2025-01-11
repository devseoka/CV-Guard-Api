namespace Cv.Guard.Api.Contracts.Services
{
	public interface IUploadService
	{
		Task<string> UploadAsync(Stream stream, string name);
		Task<MemoryStream> DownloadAsync(string uri);
	}
}
