﻿using DataProvider;
using DevExpress.Persistent.Base;
using System;
using System.Configuration;

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

        }

        protected void Application_Error(object sender, EventArgs e) {

        }

        protected void Session_End(object sender, EventArgs e) {
            
        }

        protected void Application_End(object sender, EventArgs e) {

        }
    }
}