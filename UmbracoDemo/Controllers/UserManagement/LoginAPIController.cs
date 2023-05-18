using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;
using UmbracoDemo.ViewModels;

namespace UmbracoDemo.Controllers.UserManagement
{
    public class LoginAPIController : RenderController
    {
        private readonly IMemberManager _memberManager;
        public LoginAPIController(IMemberManager memberManager,
            ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor)
            : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _memberManager = memberManager;
        }

        public override IActionResult Index()
        {
            var loginPage = CurrentPage as LoginApi;
            LoginViewModel viewModel = new()
            {
                PageTitle = loginPage.PageTitle,
                IsLoggedIn = _memberManager.IsLoggedIn()
            };
            return View("~/Views/LoginAPI.cshtml");
        }
    }

}
