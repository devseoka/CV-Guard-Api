using AutoMapper;
using Cv.Guard.Api.Core.Models;
using IpStack.Models;

namespace Cv.Guard.Api.Helpers
{
	public class GlobalMappingProfile : Profile
	{
		public GlobalMappingProfile()
		{
			CreateMap<IFormFile, Upload>()
				.ForMember((dst) => dst.Name, opts => opts.MapFrom(src => src.FileName))
				.ForMember((dst) => dst.MimeType, opts => opts.MapFrom(src => src.ContentType))
				.ForMember((dst) => dst.Extension, opts => opts.MapFrom(src => Path.GetExtension(src.FileName)))
				.AfterMap(
					(src, dst) =>
					{
						dst.Size = Math.Round((double)(src.Length / 1024), 2);
					}
				);
			CreateMap<IpAddressDetails, Core.Models.Location>()
				.ForMember((dst) => dst.Region, opts => opts.MapFrom((src) => src.RegionName))
				.ForMember((dst) => dst.Host, opts => opts.MapFrom((src) => src.Hostname))
				.ForMember((dst) => dst.Country, opts => opts.MapFrom((src) => src.CountryName));
		}
	}
}
