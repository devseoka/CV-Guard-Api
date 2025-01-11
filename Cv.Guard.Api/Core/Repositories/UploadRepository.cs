using Cv.Guard.Api.Contracts.Repositories;
using Cv.Guard.Api.Core.Models;

public class UploadRepository : IUploadRepository
{
	public IQueryable<Upload> Uploads => throw new NotImplementedException();

	public Task<Upload> Add(Upload upload)
	{
		throw new NotImplementedException();
	}

	public Task<Upload> GetUploadByKey(string key)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Upload>> GetUploads()
	{
		throw new NotImplementedException();
	}
}
