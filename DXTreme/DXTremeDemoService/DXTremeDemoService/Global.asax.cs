using MainDemo.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace DXTremeDemoService {
    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {
            ODataDemoServiceOptions.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;// "User ID=Test4;Password=1tr.erweerT;Pooling=false;Data Source=minakov-w8;Initial Catalog=MainDemo_v12.1";
            ODataDemoServiceOptions.NamespaceName = "MainDemo";
            ODataDemoServiceOptions.Assembly = typeof(Contact).Assembly;
        }

        protected void Session_Start(object sender, EventArgs e) {

        }

        protected void Application_BeginRequest(object sender, EventArgs e) {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) {

        }

        protected void Application_Error(object sender, EventArgs e) {

        }

        protected void Session_End(object sender, EventArgs e) {

        }

        protected void Application_End(object sender, EventArgs e) {

        }
    }
}