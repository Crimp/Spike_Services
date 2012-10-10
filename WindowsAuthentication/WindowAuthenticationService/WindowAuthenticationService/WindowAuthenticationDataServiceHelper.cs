using DataProvider;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using System.Reflection;

namespace WindowAuthenticationService {
    public class WindowAuthenticationDataServiceHelper : DataServiceHelper {
        public WindowAuthenticationDataServiceHelper(string connectionString, Assembly[] assemblies, string namespaceName) :
            base(connectionString, assemblies, namespaceName) {
        }
        protected override ISelectDataSecurity GetSelectDataSecurity() {
            SecurityStrategyComplex securityStrategy = new SecurityStrategyComplex(typeof(SecuritySystemUser), typeof(DevExpress.ExpressApp.Security.Strategy.SecuritySystemRole), new AuthenticationActiveDirectory());
            SecuritySystem.SetInstance(securityStrategy);
            SecuritySystem.Instance.Logon(ObjectSpaceProvider.CreateObjectSpace());
            return securityStrategy.CreateSelectDataSecurity();
        }
    }
}