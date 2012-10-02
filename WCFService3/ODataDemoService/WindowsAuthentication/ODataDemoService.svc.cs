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

namespace WindowsAuthentication {
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class DemoODataDemoService : ODataDemoServiceBase {
        public DemoODataDemoService() {
        }
        public DemoODataDemoService(HttpContextBase httpContext)
            : base(httpContext) {
        }
    }
}
