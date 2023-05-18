using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Strings;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.Filters;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Cms.Web.Website.Controllers;
using Umbraco.Extensions;
using UmbracoDemo.Dtos;
using UmbracoDemo.Services.Interfaces;
using static Umbraco.Cms.Core.Constants.Conventions;
using Folder = Umbraco.Cms.Web.Common.PublishedModels.Folder;

namespace UmbracoDemo.Controllers
{
    public class ArticleController : SurfaceController
    {
        private readonly IPublishedSnapshotAccessor _snapshotAccessor;
        private readonly IContentService _contentService;
        private readonly IUploadedFileService _uploadedFileService;
        private readonly UmbracoHelper _umbracoHelper;
        private readonly IMediaService _mediaService;
        private readonly MediaFileManager _mediaFileManager;
        private readonly MediaUrlGeneratorCollection _mediaUrlGeneratorCollection;
        private readonly IShortStringHelper _shortStringHelper;
        private readonly IContentTypeBaseServiceProvider _contentTypeBaseServiceProvider;
        public ArticleController(IPublishedSnapshotAccessor snapshotAccessor,
                                 IContentService contentService,
                                  IUploadedFileService uploadedFileService,
                                  UmbracoHelper umbracoHelper,
                                 IMediaService mediaService,
                                 MediaUrlGeneratorCollection mediaUrlGeneratorCollection,
                                 MediaFileManager mediaFileManager,
                                 IShortStringHelper shortStringHelper,
                                 IContentTypeBaseServiceProvider contentTypeBaseServiceProvider,

            IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _uploadedFileService = uploadedFileService;
            _snapshotAccessor = snapshotAccessor;
            _contentService = contentService;
            _umbracoHelper = umbracoHelper;
            _mediaService = mediaService;
            _mediaUrlGeneratorCollection = mediaUrlGeneratorCollection;
            _shortStringHelper = shortStringHelper;
            _contentTypeBaseServiceProvider = contentTypeBaseServiceProvider;
            _mediaFileManager = mediaFileManager;
        }



        public IActionResult Form()
        {
            var articleToAdd = new ArticleToAddDto();
            return PartialView("ArticleForm", articleToAdd);
        }


        //[HttpPost]
        ////Can be accessed througth umbraco only 
        //[ValidateUmbracoFormRouteString]
        //public async Task<IActionResult> Submit(ArticleToAddDto articleToAdd)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ModelState.AddModelError("", "The model is not valid");
        //        return RedirectToCurrentUmbracoPage();
        //    }

        //    #region Create Content
        //    //Create a variable for the GUID of the parent where you want to add a child item(Articles)
        //    var parentId = new Guid("753f9547-d41a-4ee0-bae8-91c160bc08f1");
        //   //Create service for the article content
        //    var articleContent = _contentService.Create(articleToAdd.Title, parentId, Article.ModelTypeAlias);
        //    articleContent.SetCultureName(articleToAdd.Title, "en-US");

        //    //Assign data to content properties
        //    var titleProperty = Article.GetModelPropertyType(_snapshotAccessor, a => a.Title);
        //    articleContent.SetValue(titleProperty?.Alias, articleToAdd.Title, "en-US");
        //    var briefProperty = Article.GetModelPropertyType(_snapshotAccessor, a => a.Brief);
        //    articleContent.SetValue(briefProperty?.Alias, articleToAdd.Brief, "en-US");

        //    //Deal with DateTime and Dateonly
        //    var dateFromProperty = Article.GetModelPropertyType(_snapshotAccessor, a => a.DateFrom);
        //    articleContent.SetValue(dateFromProperty.Alias, articleToAdd.DateFrom);

        //    var dateToProperty = Article.GetModelPropertyType(_snapshotAccessor, a => a.DateTo);
        //    articleContent.SetValue(dateToProperty.Alias, articleToAdd.DateTo);

        //    //Deal with file upload and media picker
        //    var featuredImageProperty = Article.GetModelPropertyType(_snapshotAccessor, a => a.FeaturedImage);
        //    var mediaImageProperty = Article.GetModelPropertyType(_snapshotAccessor, a => a.MediaImage);

        //    if (articleToAdd.FeaturedImage != null)
        //    {
        //        var filePublishedContent = await _uploadedFileService.CreateFileUploadMedia(articleToAdd.FeaturedImage);
        //        if(filePublishedContent != null)
        //            articleContent.SetValue(featuredImageProperty?.Alias, filePublishedContent.Url());
        //        #endregion
        //    }
        //    if (articleToAdd.MediaImage != null)
        //    {
        //        #region Media Picker
        //        var imageMedia = await _uploadedFileService.CreateMediaPicker(articleToAdd.MediaImage);
        //        if(imageMedia != null)
        //            articleContent.SetValue(mediaImageProperty.Alias, imageMedia.GetUdi().ToString());
        //        #endregion
        //    }
        //    //Save and publish content
        //    _contentService.SaveAndPublish(articleContent);
        //    return RedirectToUmbracoPage(parentId);
        //}


    }
}
