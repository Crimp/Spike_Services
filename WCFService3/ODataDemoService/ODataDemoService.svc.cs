using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using DevExpress.Xpo;
using System.ServiceModel;
using System.ServiceModel.Activation;
using DevExpress.Xpo.DB;
using MainDemo.Module.BusinessObjects;
using DevExpress.ExpressApp.Security;

[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
public class MainDemoODataDemoService : ODataDemoServiceBase {
    static MainDemoODataDemoService() {
        ConnectionString = "User ID=Test4;Password=1tr.erweerT;Pooling=false;Data Source=minakov-w8;Initial Catalog=MainDemo_v12.1";
        NamespaceName = "MainDemo";
        Assembly = typeof(Contact).Assembly;
    }
    public MainDemoODataDemoService(){
    }
    public MainDemoODataDemoService(HttpContextBase httpContext)
        : base(httpContext) {
    }
}
