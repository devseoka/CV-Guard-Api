using Cv.Guard.Api.Core.Models;

namespace Cv.Guard.Api.Contracts.Repositories
{
	public interface ILocationRepository
	{
		IQueryable<Location> Locations { get; }
		Task<Location> Add(Location location);
	}
}
