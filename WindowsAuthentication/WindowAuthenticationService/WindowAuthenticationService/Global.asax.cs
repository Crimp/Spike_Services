﻿using BusinessObjectsLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace WindowAuthenticationService {
    public class Global : System.Web.HttpApplication {
        public static string ConnectionString;

        protected void Application_Start(object sender, EventArgs e) {
            ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            DBUpdater updater = new DBUpdater();
            updater.Update(ConnectionString, new Assembly[] { typeof(Contact).Assembly });
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