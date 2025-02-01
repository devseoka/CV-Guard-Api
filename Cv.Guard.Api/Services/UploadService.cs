using Azure.Storage.Blobs;
using Cv.Guard.Api.Configuration;
using Cv.Guard.Api.Contracts.Services;
using Cv.Guard.Api.Core.Exceptions;
using Microsoft.Extensions.Options;

namespace Cv.Guard.Api.Services
{
	public class UploadService(BlobServiceClient azure, IOptions<AzureBlobConfig> options) : IUploadService
	{
		private readonly AzureBlobConfig _blobOptions = options.Value;

		public async Task<MemoryStream> DownloadAsync(string uri)
		{
			var containerClient = azure.GetBlobContainerClient(_blobOptions.Name);
			string name = Path.GetFileName(uri);
			var blobClient = containerClient.GetBlobClient(name);
			if (!await blobClient.ExistsAsync())
			{
				throw new NotFoundException("Unable to find the requested file");
			}
			var stream = new MemoryStream();
			await blobClient.DownloadToAsync(stream);
			stream.Position = 0;
			return stream;
		}

		public async Task<string> UploadAsync(Stream stream, string name)
		{
			var containerClient = await GetContainerAsync(_blobOptions.Name);
			var blobClient = containerClient.GetBlobClient(name);

			var response = await blobClient.UploadAsync(stream, overwrite: true);
			return blobClient.Uri.ToString();
			throw new NotImplementedException();
		}

		private async Task<BlobContainerClient> GetContainerAsync(string name)
		{
			BlobContainerClient blobContainerClient = azure.GetBlobContainerClient(name);
			await blobContainerClient.CreateIfNotExistsAsync();
			return blobContainerClient;
		}
	}
}
