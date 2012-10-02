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

[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
public class ODataDemoServiceBase : XpoDataService, System.Data.Services.IRequestHandler {
    private static XPDictionary _xPDictionary = null;

    public ODataDemoServiceBase()
        : this(new HttpContextWrapper(HttpContext.Current)) {
    }
    public ODataDemoServiceBase(HttpContextBase httpContext) :
        base(new MyContext("XpoContext", ODataDemoServiceOptions.NamespaceName, CreateDataLayer())) {
        if((httpContext == null) && (HttpContext.Current == null)) {
            throw new ArgumentNullException("context", "The context cannot be null if not running on a Web context.");
        }
    }

    private static IObjectLayer CreateDataLayer() {
        SessionObjectLayer sessionObjectLayer = new SessionObjectLayer(CreateSession(), true, null, new SecurityRuleProvider(XPDictionary, CreateSelectDataSecurity()), null);
        return sessionObjectLayer;
    }
    private static XPDictionary XPDictionary {
        get {
            if(_xPDictionary == null) {
                _xPDictionary = new ReflectionDictionary();
                _xPDictionary.GetDataStoreSchema(ODataDemoServiceOptions.Assembly);
            }
            return _xPDictionary;
        }
    }

    public static UnitOfWork CreateSession() {
        IDataStore store = XpoDefault.GetConnectionProvider(ODataDemoServiceOptions.ConnectionString, DevExpress.Xpo.DB.AutoCreateOption.SchemaAlreadyExists);

        IDataLayer directDataLayer = new ThreadSafeDataLayer(XPDictionary, store);
        UnitOfWork directSession = new UnitOfWork(directDataLayer);
        return directSession;
    }

    private static ISelectDataSecurity CreateSelectDataSecurity() {
        UnitOfWork session = CreateSession();
        string userName = HttpContext.Current.User.Identity.Name;
        IOperationPermissionProvider user = session.FindObject(typeof(SecuritySystemUser), new BinaryOperator("UserName", userName)) as IOperationPermissionProvider;
        return new SelectDataSecurity(user);
    }
    public static void InitializeService(DataServiceConfiguration config) {
        config.SetEntitySetAccessRule("*", EntitySetRights.All);
        config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
        config.DataServiceBehavior.AcceptProjectionRequests = true;
    }
}
