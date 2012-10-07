using System.Web;
using System.ServiceModel.Activation;
using DataProvider;
using BusinessObjectsLibrary;

[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
public class DemoODataDemoService : ODataDemoServiceBase {
    public DemoODataDemoService()
        :this(new HttpContextWrapper(HttpContext.Current)){
    }
    public DemoODataDemoService(HttpContextBase httpContext)
        : base(httpContext, new ODataServiceHelper(WindowAuthenticationService.Global.ConnectionString, typeof(Contact).Assembly, "MainDemo")) {
    }
    public DemoODataDemoService(HttpContextBase httpContext, ODataServiceHelper dataServiceHelper)
        : base(httpContext, dataServiceHelper) {
    }
}
