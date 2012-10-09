using System.Web;
using System.ServiceModel.Activation;
using DataProvider;
using BusinessObjectsLibrary;
using System.Reflection;
using WindowAuthenticationService;

[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
public class DemoODataDemoService : ODataDemoServiceBase {
    public DemoODataDemoService()
        :this(new HttpContextWrapper(HttpContext.Current)){
    }
    public DemoODataDemoService(HttpContextBase httpContext)
        : base(httpContext, new WindowAuthenticationDataServiceHelper(WindowAuthenticationService.Global.ConnectionString, new Assembly[] { typeof(Contact).Assembly }, "BusinessObjectsLibrary")) {
    }
    public DemoODataDemoService(HttpContextBase httpContext, ODataServiceHelper dataServiceHelper)
        : base(httpContext, dataServiceHelper) {
    }
}
