using System.Web;
using System.ServiceModel.Activation;
using DataProvider;
using BusinessObjectsLibrary;
using System.Reflection;
using WindowAuthenticationService;

[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
public class WindowAuthenticationDataService : DataServiceBase {
    public WindowAuthenticationDataService()
        :this(new HttpContextWrapper(HttpContext.Current)){
    }
    public WindowAuthenticationDataService(HttpContextBase httpContext)
        : this(httpContext, new WindowAuthenticationDataServiceHelper(WindowAuthenticationService.Global.ConnectionString, new Assembly[] { typeof(Contact).Assembly }, "BusinessObjectsLibrary"), "WindowAuthenticationDataService") {
    }
    public WindowAuthenticationDataService(HttpContextBase httpContext, DataServiceHelper dataServiceHelper, string containerName)
        : base(httpContext, dataServiceHelper, containerName) {
    }
}
