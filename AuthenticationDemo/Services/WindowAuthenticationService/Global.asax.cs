using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Persistent.Base;
using ODataDemoServiceBase;
using System;
using System.Configuration;
using System.Web;

namespace WindowAuthenticationService {
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
            SecurityStrategyComplex securityStrategy = new SecurityStrategyComplex(typeof(SecuritySystemUser), typeof(SecuritySystemRole), new AuthenticationActiveDirectory());
            SecuritySystem.SetInstance(securityStrategy);
            WindowAuthenticationDataServiceHelper hellper = new WindowAuthenticationDataServiceHelper();
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