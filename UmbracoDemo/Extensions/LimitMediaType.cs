using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace UmbracoDemo.Extensions
{
    public class LimitMediaType : INotificationHandler<ContentSavingNotification>
    {

        public void Handle(ContentSavingNotification notification)
        {
            //Add condition
            foreach (var entity in notification.SavedEntities)
            {
                var prop = entity.Properties;
            }
        }
    }

}
