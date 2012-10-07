using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.Web;
using DevExpress.Xpo;
using System.ServiceModel;
using System.ServiceModel.Activation;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp.Security;
using System.Reflection;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Security.Strategy;

namespace DataProvider {
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class ODataDemoServiceBase : XpoDataService, System.Data.Services.IRequestHandler {
        public ODataDemoServiceBase(HttpContextBase httpContext, ODataServiceHelper dataServiceHelper) :
            base(new XpoContext("XpoContext", dataServiceHelper.NamespaceName, dataServiceHelper.CreateDataLayer())) {
            if((httpContext == null) && (HttpContext.Current == null)) {
                throw new ArgumentNullException("context", "The context cannot be null if not running on a Web context.");
            }
        }
        public static void InitializeService(DataServiceConfiguration config) {
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
            config.DataServiceBehavior.AcceptProjectionRequests = true;
        }
    }
}
