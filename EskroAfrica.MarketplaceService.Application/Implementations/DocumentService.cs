using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EskroAfrica.MarketplaceService.Application.Interfaces;
using EskroAfrica.MarketplaceService.Common.DTOs.Requests;
using EskroAfrica.MarketplaceService.Common.DTOs.Response;
using System.Net;

namespace EskroAfrica.MarketplaceService.Application.Implementations
{
    public class DocumentService : IDocumentService
    {
        private readonly ILogService _logService;
        private readonly Cloudinary _cloudinary;
        private readonly IJwtTokenService _jwtTokenService;

        public DocumentService(ILogService logService, Cloudinary cloudinary, IJwtTokenService jwtTokenService)
        {
            _logService = logService;
            _cloudinary = cloudinary;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<ApiResponse<List<string>>> Upload(DocumentRequest request)
        {
            var apiResponse = new ApiResponse<List<string>>();
            var urls = new List<string>();

            foreach(var file in request.Files)
            {
                if (file.Length == 0) continue;

                if (file.ContentType.Contains("image"))
                {
                    var imageResult = await _cloudinary.UploadAsync(new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName,
                            file.OpenReadStream()),
                        Tags = _jwtTokenService.IdentityUserId
                    }).ConfigureAwait(false);

                    if(imageResult != null && imageResult.StatusCode == HttpStatusCode.OK)
                    {
                        urls.Add(imageResult.SecureUrl.ToString());
                    }
                }else if (file.ContentType.Contains("video"))
                {
                    var videoResult = await _cloudinary.UploadAsync(new VideoUploadParams
                    {
                        File = new FileDescription(file.FileName,
                            file.OpenReadStream()),
                        Tags = _jwtTokenService.IdentityUserId
                    }).ConfigureAwait(false);

                    if (videoResult != null && videoResult.StatusCode == HttpStatusCode.OK)
                    {
                        urls.Add(videoResult.SecureUrl.ToString());
                    }
                }
            }

            return urls.Any() ? apiResponse.Success(urls, "success")
                : apiResponse.Failure("Could not upload files");
        }

        public async Task Download(string filePath)
        {

        }
    }
}
