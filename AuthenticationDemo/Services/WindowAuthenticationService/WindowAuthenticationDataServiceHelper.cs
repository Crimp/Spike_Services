using BusinessObjectsLibrary;
using DataProvider;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using System.Reflection;

namespace WindowAuthenticationService {
    public class WindowAuthenticationDataServiceHelper : DataServiceHelper {
        public WindowAuthenticationDataServiceHelper() :
            this(Global.ConnectionString, new Assembly[] { typeof(Contact).Assembly }, "BusinessObjectsLibrary") {
        }
        public WindowAuthenticationDataServiceHelper(string connectionString, Assembly[] assemblies, string namespaceName) :
            base(connectionString, assemblies, namespaceName) {
        }
        protected override ISelectDataSecurity GetSelectDataSecurity() {
            return ((SecurityStrategy)SecuritySystem.Instance).CreateSelectDataSecurity();
        }
    }
}