using System.Net;
using System.Text;
using AutoMapper;
using Azure.Storage.Blobs;
using Cv.Guard.Api.Configuration;
using Cv.Guard.Api.Contracts.Repositories;
using Cv.Guard.Api.Contracts.Services;
using Cv.Guard.Api.Core.Atrributes;
using Cv.Guard.Api.Core.Dto;
using Cv.Guard.Api.Core.Exceptions;
using Cv.Guard.Api.Core.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PostmarkDotNet;

namespace Cv.Guard.Api.Controllers
{
	public class CvController(
		IValidator<UploadRequest> uploadValidator,
		IValidator<EmailRequest> emailValidator,
		IEmailRepository emailRepository,
		IUploadRepository uploadRepository,
		ILocationRepository locationRepository,
		IUploadService uploadService,
		IEmailService emailService,
		ILocationService locationService,
		IMapper mapper,
		IOptions<PostmarkConfig> options
	) : ApiBaseController
	{
		private readonly PostmarkConfig _postmarkConfig = options.Value;

		[HttpPost]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> Upload(IFormFile file, [FromQuery] string initials)
		{
			uploadValidator.ValidateAndThrow(new UploadRequest { File = file, Initials = initials });

			var upload = mapper.Map<Upload>(file);
			var firstRandomSegment = GenerateRandomString(8);
			var secondRandomSegment = GenerateRandomString(8);
			var unixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

			var uri = await uploadService.UploadAsync(file.OpenReadStream(), $"{unixTimestamp}_{file.FileName}");
			upload.Path = uri;
			var sb = new StringBuilder();
			sb.Append(initials)
				.Append('-')
				.Append(unixTimestamp)
				.Append('-')
				.Append(firstRandomSegment)
				.Append('-')
				.Append(secondRandomSegment);
			upload.Key = sb.ToString();
			await uploadRepository.Add(upload);

			return Ok(
				new
				{
					ApiKey = upload.Key,
					FileUri = uri,
					Status = !string.IsNullOrEmpty(uri),
				}
			);
		}

		[HttpPost("download")]
		[ApiKey]
		public async Task<IActionResult> Download([FromBody] EmailRequest request)
		{
			emailValidator.ValidateAndThrow(request);
			string domain = request.Email.Split('@').Last();

			var ipAddresses = Dns.GetHostAddresses(domain);
			if (ipAddresses.Length < 1)
			{
				string message = $"Domain name of this {request.Email} email is invalid or has been deregistered";
				throw new BadRequestException(message, errors: [message]);
			}
			var ipAddress = GetClientIpAddress(Request);
			var loc = await locationService.GetLocationByIpAddress(ipAddress);
			var location = mapper.Map<Location>(loc);

			await locationRepository.Add(location);

			if (!Request.Headers.TryGetValue("X-API-Key", out var key))
			{
				throw new UnauthorizedException("API key is required.");
			}

			var upload = await uploadRepository.GetUploadByKey(key);

			if (upload is null || upload.Path is null)
			{
				throw new NotFoundException("You don't have any files uploaded. Upload your curriculum vitae first.");
			}

			var stream = await uploadService.DownloadAsync(upload.Path);
			var content = stream.ToArray();
			string name = Path.GetFileName(upload.Path);

			var templateMessage = new TemplatedPostmarkMessage
			{
				To = request.Email,
				From = _postmarkConfig.Sender,
				TemplateAlias = "cv",
				TemplateModel = new Dictionary<string, object>
				{
					{ "name", request.Name },
					{ "action_url", upload.Path },
				},
			};
			var attachment = new List<PostmarkMessageAttachment>
			{
				new()
				{
					Name = name,
					Content = Convert.ToBase64String(content),
					ContentType = upload.MimeType,
				},
			};

			templateMessage.Attachments = attachment;
			var response = await emailService.SendAsync(templateMessage);
			var email = new Email
			{
				Data = JsonConvert.SerializeObject(templateMessage),
				Message = response.Message,
				Status = response.Status == PostmarkStatus.Success,
			};
			await emailRepository.Add(email);
			return File(stream, upload.MimeType, name, true);
		}

		private static string GenerateRandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			var random = new Random();
			var stringBuilder = new StringBuilder(length);

			for (int i = 0; i < length; i++)
			{
				stringBuilder.Append(chars[random.Next(chars.Length)]);
			}

			return stringBuilder.ToString();
		}
		private static string GetClientIpAddress(HttpRequest request)
		{
			if (request.Headers.TryGetValue("X-Forwarded-For", out var forwardedFor) &&
				!string.IsNullOrWhiteSpace(forwardedFor.ToString()))
			{
				var ips = forwardedFor.ToString().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(ip => ip.Trim());
				foreach (var ip in ips)
				{
					if (IPAddress.TryParse(ip, out _))
					{
						return ip;
					}
				}
			}
			var remoteIp = request.HttpContext.Connection.RemoteIpAddress;
			if (remoteIp != null)
			{
				return remoteIp.IsIPv4MappedToIPv6 ? remoteIp.MapToIPv4().ToString() : remoteIp.ToString();
			}
			throw new BadRequestException("Unable to determine the client's IP address. No valid IP address was found in the request.");
		}

	}
}
