using DataProvider;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Persistent.Base.Security;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;
using System.Web.Security;

namespace Authentication {
    //[JavascriptCallbackBehavior(UrlParameterName = "$callback")]
    public abstract class CustomAuthenticationServiceBase : ICustomAuthenticationService {
        public CustomAuthenticationServiceBase() {
        }
        public string ValidateUser(string userName, string password) {
            UnitOfWork session = Session;
            IAuthenticationStandardUser user = session.FindObject(typeof(SecuritySystemUser), new BinaryOperator("UserName", userName)) as IAuthenticationStandardUser;
            string result = null;
            if(user != null && user.ComparePassword(password)) {
                FormsAuthenticationTicket ticket =
                    new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(30)
                        , false, password, FormsAuthentication.FormsCookiePath);

                result = FormsAuthentication.Encrypt(ticket);
            }
            return result;
        }
        protected abstract UnitOfWork Session {
            get;
        }
    }
}