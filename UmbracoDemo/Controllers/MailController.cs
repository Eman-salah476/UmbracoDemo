using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;

namespace UmbracoDemo.Controllers
{
    [Route("api/Mail/")]
    public class MailController : UmbracoApiController
    {
        public MailController()
        {

        }

        [HttpPost("{message}")]
        public IActionResult SendMail(string message)
        {
            //Send Mail
            return Ok($"Sent {message}");
        }
                   
    }
}
