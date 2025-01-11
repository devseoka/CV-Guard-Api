using Cv.Guard.Api.Contracts.Services;
using IpStack.Models;
using IpStack.Services;

namespace Cv.Guard.Api.Services
{
	public class LocationService(IIpStackService ipStackService) : ILocationService
	{
		public async Task<IpAddressDetails> GetLocationByIpAddress(string ip)
		{
			var location = await ipStackService.GetIpAddressDetailsAsync(ipAddress: ip);
			return location;
		}
	}
}
