using Umbraco.Cms.Core.Notifications;
using UmbracoDemo.Extensions;

namespace UmbracoDemo.Configurations
{
    public static class NotificationsConfiguration
    {
        public static IUmbracoBuilder AddCustomNotificationHandler(this IUmbracoBuilder builder)
        {
            builder.AddNotificationHandler<MediaSavingNotification, LimitUploadedFileSize>()
                .AddNotificationHandler<ContentMovingToRecycleBinNotification, PreventDeleteUsedNodes>()
                .AddNotificationHandler<ContentSavingNotification, LimitMediaType>();

            return builder;
        }
    }
}
