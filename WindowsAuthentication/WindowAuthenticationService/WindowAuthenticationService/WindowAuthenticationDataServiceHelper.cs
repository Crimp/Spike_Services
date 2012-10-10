using DataProvider;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WindowAuthenticationService {
    public class WindowAuthenticationDataServiceHelper : ODataServiceHelper {
        public WindowAuthenticationDataServiceHelper(string connectionString, Assembly[] assemblies, string namespaceName) :
            base(connectionString, assemblies, namespaceName) {
        }
    }
}