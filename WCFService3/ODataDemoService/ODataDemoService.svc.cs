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
using DataProvider;
using ODataDemoService;
using System.Reflection;

[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
public class DemoODataDemoService : ODataDemoServiceBase {
    public DemoODataDemoService()
        :this(new HttpContextWrapper(HttpContext.Current)){
    }
    public DemoODataDemoService(HttpContextBase httpContext)
        : base(httpContext, new ODataServiceHelper(Global.ConnectionString, new Assembly[] { typeof(Contact).Assembly }, "MainDemo")) {
    }
    public DemoODataDemoService(HttpContextBase httpContext, ODataServiceHelper dataServiceHelper)
        : base(httpContext, dataServiceHelper) {
    }
}
