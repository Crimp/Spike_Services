namespace MainDemoWebClient.UserAccountWrappers {
    using System.Web;
    using System.Web.Security;
    using DevExpress.Xpo;

    public static class MembershipHelper {
        public static string UserName {
            get {
                string ticketValue = null;

                var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if(cookie != null) {
                    ticketValue = cookie.Value;
                }

                if(!string.IsNullOrEmpty(ticketValue)) {
                    FormsAuthenticationTicket ticket;

                    try {
                        ticket = FormsAuthentication.Decrypt(ticketValue);
                    }
                    catch {
                        return string.Empty;
                    }

                    if(ticket != null) {
                        var identity = new FormsIdentity(ticket);

                        return identity.Name;
                    }
                    else {
                        return string.Empty;
                    }
                }
                else {
                    return string.Empty;
                }
            }
        }
        public static bool IsUserLoggedIn() {
            return !string.IsNullOrWhiteSpace(UserName);
        }
    }
}