using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Persistent.Base;
using ODataDemoServiceBase;
using System;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CustomAuthenticationService {
    public class Global : System.Web.HttpApplication {
        public static string ConnectionString;
        protected void Application_Start(object sender, EventArgs e) {
            ValueManager.ValueManagerType = typeof(ASPRequestValueManager<>).GetGenericTypeDefinition();
            ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        }

        protected void Session_Start(object sender, EventArgs e) {

        }

        protected void Application_BeginRequest(object sender, EventArgs e) {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) {
            SecurityStrategyComplex securityStrategy = new SecurityStrategyComplex(typeof(SecuritySystemUser), typeof(SecuritySystemRole), new AuthenticationStandard());
            SecuritySystem.SetInstance(securityStrategy);
            // Remember claims based security should be only be 
            // used over HTTPS  
            //if(context.Request.IsSecureConnection){
            if(HttpContext.Current.Request.Headers.AllKeys.Contains("UserName")) {
                string userName = HttpContext.Current.Request.Headers["UserName"];
                ((AuthenticationStandardLogonParameters)SecuritySystem.LogonParameters).UserName = userName;
            }
            if(HttpContext.Current.Request.Headers.AllKeys.Contains("Password")) {
                string password = HttpContext.Current.Request.Headers["Password"];
                ((AuthenticationStandardLogonParameters)SecuritySystem.LogonParameters).Password = password;
            }
            //}
            CustomAuthenticationServiceHelper hellper = new CustomAuthenticationServiceHelper();
            try {
                SecuritySystem.Instance.Logon(hellper.ObjectSpaceProvider.CreateObjectSpace());
            }
            catch(AuthenticationException) {
                HttpContext.Current.Response.Status = "401 Unauthorized";
                HttpContext.Current.Response.StatusCode = 401;
                HttpContext.Current.Response.End();
            }
        }

        protected void Application_Error(object sender, EventArgs e) {

        }

        protected void Session_End(object sender, EventArgs e) {

        }

        protected void Application_End(object sender, EventArgs e) {

        }
    }
}