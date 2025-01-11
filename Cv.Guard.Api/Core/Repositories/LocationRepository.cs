using Cv.Guard.Api.Contracts.Repositories;
using Cv.Guard.Api.Core.Models;

namespace Cv.Guard.Api.Core.Repositories;

public class LocationRepository : ILocationRepository
{
	public IQueryable<Location> Locations => throw new NotImplementedException();

	public Task<Location> Add(Location location)
	{
		throw new NotImplementedException();
	}
}
