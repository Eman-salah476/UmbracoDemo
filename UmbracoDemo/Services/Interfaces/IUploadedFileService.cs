using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoDemo.Services.Interfaces
{
    public interface IUploadedFileService
    {
        Task<string> SaveFile(IFormFile file);
        Task<IPublishedContent> CreateFileUploadMedia(IFormFile addedFile);
        Task<IMedia> CreateMediaPicker(IFormFile addedImage);
    }
}
