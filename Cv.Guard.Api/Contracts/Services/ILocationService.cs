using IpStack.Models;

namespace Cv.Guard.Api.Contracts.Services
{
	public interface ILocationService
	{
		Task<IpAddressDetails> GetLocationByIpAddress(string ip);
	}
}
