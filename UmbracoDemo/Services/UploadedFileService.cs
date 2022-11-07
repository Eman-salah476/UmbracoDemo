using System.IO;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Strings;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.PublishedModels;
using UmbracoDemo.Services.Interfaces;
using File = Umbraco.Cms.Web.Common.PublishedModels.File;

namespace UmbracoDemo.Services
{
    public class UploadedFileService : IUploadedFileService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly UmbracoHelper _umbracoHelper;
        private readonly IMediaService _mediaService;
        private readonly MediaFileManager _mediaFileManager;
        private readonly MediaUrlGeneratorCollection _mediaUrlGeneratorCollection;
        private readonly IShortStringHelper _shortStringHelper;
        private readonly IContentTypeBaseServiceProvider _contentTypeBaseServiceProvider;

        public UploadedFileService(IWebHostEnvironment environment,
                                 UmbracoHelper umbracoHelper,
                                 IMediaService mediaService,
                                 MediaUrlGeneratorCollection mediaUrlGeneratorCollection,
                                 MediaFileManager mediaFileManager,
                                 IShortStringHelper shortStringHelper,
                                 IContentTypeBaseServiceProvider contentTypeBaseServiceProvider)
        {
            _environment = environment;
            _umbracoHelper = umbracoHelper;
            _mediaService = mediaService;
            _mediaUrlGeneratorCollection = mediaUrlGeneratorCollection;
            _shortStringHelper = shortStringHelper;
            _contentTypeBaseServiceProvider = contentTypeBaseServiceProvider;
            _mediaFileManager = mediaFileManager;
        }


        public async Task<string> SaveFile(IFormFile uploadedFile)
        {
            var wwwRoot = _environment.WebRootPath;
            var folderPath = Path.Combine(wwwRoot, "assets", "files");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var filePath = Path.Combine(folderPath, uploadedFile.FileName);

            try
            {
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await uploadedFile.CopyToAsync(fileStream);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                throw;
            }

            return filePath;
        }


        /// <summary>
        /// This function will create media of type upload file to save featured image in it  
        /// </summary>
        /// <param name="addedFile" type="IFormfile"></param>
        /// <returns name="publishedContent" type="IPublishedContent"></returns>
        public async Task<IPublishedContent> CreateFileUploadMedia(IFormFile addedFile)
        {
            try
            {
                using var memorystream = new MemoryStream();
                await addedFile.CopyToAsync(memorystream);
                // Create a media file
                var folderId = new Guid("11b94adb-cf6d-4804-a8d8-d9d7f6a7fd80");
                //var createdMediaService = _mediaService.CreateMediaWithIdentity("FeaturedImages", -1, "File");
                var createdMediaService = _mediaService.CreateMedia(addedFile.FileName, folderId, File.ModelTypeAlias);

                var propertyTypeAlias = Constants.Conventions.Media.File;
                // Create a media file
                createdMediaService.SetValue(_mediaFileManager, _mediaUrlGeneratorCollection, _shortStringHelper, _contentTypeBaseServiceProvider,
                                             propertyTypeAlias, addedFile.FileName, (Stream)memorystream);
                // Save the created media 
                var isSaved = _mediaService.Save(createdMediaService);
                if (!isSaved.Success)
                    return null;
                var publishedMedia = _umbracoHelper.Media(createdMediaService.Id);
                return publishedMedia;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// This function will create media of type media picker to save Media image in it  
        /// </summary>
        /// <param name="addedFile"></param>
        /// <returns></returns>
        public async Task<IMedia> CreateMediaPicker(IFormFile addedImage)
        {
            //Create stream 
            using var memoryStream = new MemoryStream();
            await addedImage.CopyToAsync(memoryStream);
            //Create Folder to add image in if there is no folder.
            //var folder = _mediaService.CreateMedia("MediaImage", -1, Folder.ModelTypeAlias);
            //_mediaService.Save(folder);
            //var folderUdi = folder.GetUdi().Guid;

            //I created folder to save in it 
            var folderUdi = new Guid("8c0a114d-ba03-4312-b8f7-cf826fb7a04b");

            var imageMedia = _mediaService.CreateMedia(addedImage.FileName, folderUdi, Image.ModelTypeAlias);
            imageMedia.SetValue(_mediaFileManager, _mediaUrlGeneratorCollection, _shortStringHelper, _contentTypeBaseServiceProvider,
                                Constants.Conventions.Media.File, addedImage.FileName, (Stream)memoryStream);

            var isSaved = _mediaService.Save(imageMedia);
            if (!isSaved.Success)
                return null;
            return imageMedia;

        }

    }
}
