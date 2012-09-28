namespace MainDemoWebClient.Controllers {
    using System;
    using System.Globalization;
    using System.Web.Mvc;
    using System.Web.Security;
    using MainDemoWebClient.Models;
    using MainDemoWebClient.UserAccountWrappers;
    using System.Web.ClientServices;
    using System.Web;
    using MainDemoWebClient.AuthenticationService;

    [HandleError]
    public class AccountController : Controller {
        public AccountController() {
        }

        public ActionResult Unauthorized() {
            return this.View();
        }

        public ActionResult LogOn() {
            return this.View(new LogOnModel());
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Justification = "Needs to take same parameter type as Controller.Redirect()")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOn(LogOnModel model, string returnUrl) {
            if(ModelState.IsValid) {
                //todo Minakov
                AuthenticationServiceClient authenticationService = new AuthenticationServiceClient();
                string result = authenticationService.ValidateUser(model.UserName, model.Password);
                FormsAuthenticationTicket ticket = string.IsNullOrEmpty(result) ? null : FormsAuthentication.Decrypt(result);
                if(ticket != null) {
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, result));
                    if(Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\")) {
                        return Redirect(returnUrl);
                    }
                    else {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else {
                    ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");
                }
            }

            return View(model);
        }

        public ActionResult LogOff() {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
