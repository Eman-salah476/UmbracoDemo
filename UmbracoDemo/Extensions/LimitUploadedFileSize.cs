using Org.BouncyCastle.Crypto.Prng;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Web.Common.PublishedModels;
using File = Umbraco.Cms.Web.Common.PublishedModels.File;

namespace UmbracoDemo.Extensions
{
    public class LimitUploadedFileSize : INotificationHandler<MediaSavingNotification>
    {
        public void Handle(MediaSavingNotification notification)
        {
            // Check Media size
            foreach (var mediaItem in notification.SavedEntities)
            {
                if (!IsMediaValid(mediaItem))
                {
                    notification.CancelOperation(new EventMessage("Error", "The size of the File exceed the limit", EventMessageType.Error));

                }
            }
        }


        private bool IsMediaValid(IMedia mediaItem)
        {
            double limit;
            double mediaSize;
            //var prop = mediaItem.Properties;
            //To Get Extension of file
            //var extension = mediaItem.Properties["umbracoExtension"]?.GetValue();
            
            //if (!mediaItem.HasProperty("umbracoBytes"))
            //    return false;
           
            var sizeInBytes = mediaItem.GetValue("umbracoBytes")?.ToString();
            double.TryParse(sizeInBytes, out mediaSize);
          
            if (mediaItem.ContentType.Alias == UmbracoMediaVideo.ModelTypeAlias)
                limit = 500 * Math.Pow(1024, 2);
            else
                limit = 2 * Math.Pow(1024, 2);

            return mediaSize <= limit;
        }

    }
}
