using DataProvider;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Persistent.Base.Security;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace TestAuthenticationService {
    public class AuthenticationServiceBase : IAuthenticationService {
        public AuthenticationServiceBase() {
        }
        public string ValidateUser(string userName, string password) {
            UnitOfWork session = ODataDemoServiceBase.CreateSession();
            IAuthenticationStandardUser user = session.FindObject(typeof(SecuritySystemUser), new BinaryOperator("UserName", userName)) as IAuthenticationStandardUser;
            string result = null;
            if(user != null && user.ComparePassword(password)) {
                FormsAuthenticationTicket ticket =
                    new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(30)
                        , false, userName, FormsAuthentication.FormsCookiePath);

                result = FormsAuthentication.Encrypt(ticket);
            }
            return result;
        }
    }
}