using AutoMapper;
using Cv.Guard.Api.Core.Models;

namespace Cv.Guard.Api.Helpers
{
	public class GlobalMappingProfile : Profile
	{
		GlobalMappingProfile()
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
		}
	}
}
