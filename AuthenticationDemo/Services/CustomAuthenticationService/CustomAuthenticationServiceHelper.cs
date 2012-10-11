using DataProvider;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;

namespace CustomAuthenticationService {
    public class CustomAuthenticationServiceHelper : DataServiceHelper {
        public CustomAuthenticationServiceHelper(string connectionString, Assembly[] assemblies, string namespaceName) :
            base(connectionString, assemblies, namespaceName) {
        }
        protected override ISelectDataSecurity GetSelectDataSecurity() {
            SecurityStrategyComplex securityStrategy = new SecurityStrategyComplex(typeof(SecuritySystemUser), typeof(DevExpress.ExpressApp.Security.Strategy.SecuritySystemRole), new AuthenticationStandard());
            SecuritySystem.SetInstance(securityStrategy);
            // Remember claims based security should be only be 
            // used over HTTPS  
            //if(context.Request.IsSecureConnection){
            if(HttpContext.Current.Request.Headers.AllKeys.Contains("UserName") && HttpContext.Current.Request.Headers.AllKeys.Contains("Password")) {
                string userName = HttpContext.Current.Request.Headers["UserName"];
                string password = HttpContext.Current.Request.Headers["Password"];
                ((AuthenticationStandardLogonParameters)SecuritySystem.LogonParameters).UserName = userName;
                ((AuthenticationStandardLogonParameters)SecuritySystem.LogonParameters).Password = password;
            }
            //}
            SecuritySystem.Instance.Logon(ObjectSpaceProvider.CreateObjectSpace());
            return securityStrategy.CreateSelectDataSecurity();
        }
    }
}