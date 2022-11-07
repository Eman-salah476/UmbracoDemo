using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.Controllers;

namespace UmbracoDemo.Extensions
{

    public class PreventDeleteUsedNodes : INotificationHandler<ContentMovingToRecycleBinNotification>
    {
        private readonly IRelationService _relationService;
        public PreventDeleteUsedNodes(IRelationService relationService)
        {
            _relationService = relationService;
        }
        public void Handle(ContentMovingToRecycleBinNotification notification)
        {
           
            foreach (var node in notification.MoveInfoCollection)
            {
                var nodeRelationAsChild = _relationService.GetByChildId(node.Entity.Id).ToList();
                if(nodeRelationAsChild != null && nodeRelationAsChild?.Count() > 0)
                    notification.CancelOperation(new EventMessage("Error", "You can't delete node that has referenced in another content", EventMessageType.Error));
            }
        }
    }




}
