using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.Web.Security;
using System.Web;
using ODataDemoService;
using DataProvider;
using DevExpress.Xpo;
using MainDemo.Module.BusinessObjects;
using System.Reflection;

public class AuthenticationService : Authentication.CustomAuthenticationService {
    public AuthenticationService() {
    }
    protected override UnitOfWork Session {
        get {
            return new ODataServiceHelper(Global.ConnectionString, new Assembly[] { typeof(Contact).Assembly }, "MainDemo").CreateSession();
        }
    }
}
