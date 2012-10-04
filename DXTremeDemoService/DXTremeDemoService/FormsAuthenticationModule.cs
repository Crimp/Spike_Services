using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Web.Security;

    public class FormsAuthenticationModule : IHttpModule {
        public void Init(HttpApplication context) {
            context.AuthenticateRequest +=
               new EventHandler(context_AuthenticateRequest);
        }
        private void context_AuthenticateRequest(object sender, EventArgs e) {
            HttpApplication app = (HttpApplication)sender;
            if(app.Context.Request.CurrentExecutionFilePath == "/ODataDemoService/ODataDemoService.svc") {
                //if(!CustomAuthenticationProvider.Authenticate(app.Context)) {
                //        app.Context.Response.Status = "401 Unauthorized";
                //        app.Context.Response.StatusCode = 401;
                //        app.Context.Response.End();
                //}
            }
        }
        public void Dispose() { }
    }
    public class CustomAuthenticationProvider {
        public static bool Authenticate(HttpContext context) {
            if(!context.Request.Headers.AllKeys.Contains("Authorization"))
                return false;

            // Remember claims based security should be only be 
            // used over HTTPS  
            if(!context.Request.IsSecureConnection)
                return false;

            string authHeader = context.Request.Headers["Authorization"];
            if(string.IsNullOrEmpty(authHeader)) {
                return false;
            }
            FormsAuthenticationTicket ticket = System.Web.Security.FormsAuthentication.Decrypt(authHeader);

            IPrincipal principal = new CustomPrincipal(ticket.UserData);
            context.User = principal;
            return true;
        }
    }
    public class CustomPrincipal : IPrincipal {
        string[] _roles;
        IIdentity _identity;

        public CustomPrincipal(string name, params string[] roles) {
            this._roles = roles;
            this._identity = new CustomIdentity(name);
        }

        public IIdentity Identity {
            get { return _identity; }
        }

        public bool IsInRole(string role) {
            return _roles.Contains(role);
        }
    }
    public class CustomIdentity : IIdentity {
        string _name;

        public CustomIdentity(string name) {
            this._name = name;
        }

        string IIdentity.AuthenticationType {
            get { return "Custom SCHEME"; }
        }

        bool IIdentity.IsAuthenticated {
            get { return true; }
        }

        string IIdentity.Name {
            get { return _name; }
        }
    }