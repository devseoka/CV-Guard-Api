using Cv.Guard.Api.Core.Models;

namespace Cv.Guard.Api.Contracts.Repositories
{
	public interface IUploadRepository
	{
		IQueryable<Upload> Uploads { get; }
		Task<Upload> Add(Upload upload);
		Task<Upload> GetUploadByKey(string id);
		Task<IEnumerable<Upload>> GetUploads();
	}
}
