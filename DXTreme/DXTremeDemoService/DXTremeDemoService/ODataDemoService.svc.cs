using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using DataProvider;

namespace DXTremeDemoService {
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    //[JSONPSupportBehavior]
    public class DemoODataDemoService : ODataDemoServiceBase {
        public DemoODataDemoService() {
        }
        public DemoODataDemoService(HttpContextBase httpContext)
            : base(httpContext) {
        }
    }
}
