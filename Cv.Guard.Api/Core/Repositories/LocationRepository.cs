using Cv.Guard.Api.Contracts.Repositories;
using Cv.Guard.Api.Core.Models;

namespace Cv.Guard.Api.Core.Repositories;

public class LocationRepository(CvgaContext context): ILocationRepository
{
	public IQueryable<Location> Locations => context.Locations;

	public async Task<Location> Add(Location location)
	{
		var entry = context.Locations.Add(location);
		await context.SaveChangesAsync();
		return entry.Entity;
	}
}
