namespace MainDemoWebClient.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            this.ViewBag.Message = HttpContext.User.Identity.Name;

            return this.View();
        }
    }
}