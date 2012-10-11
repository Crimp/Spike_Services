using BusinessObjectsLibrary;
using DataProvider;
using System.Reflection;
using System.Web;

namespace UnSecuredDataService {
    public class UnSecuredDataService : DataServiceBase {
        public UnSecuredDataService()
            : this(new HttpContextWrapper(HttpContext.Current)) {
        }
        public UnSecuredDataService(HttpContextBase httpContext)
            : this(httpContext, new DataServiceHelper(Global.ConnectionString, new Assembly[] { typeof(Contact).Assembly }, "BusinessObjectsLibrary"), "UnSecuredDataService") {
        }
        public UnSecuredDataService(HttpContextBase httpContext, DataServiceHelper dataServiceHelper, string containerName)
            : base(httpContext, dataServiceHelper, containerName) {
        }
    }
}
