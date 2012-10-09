using DataProvider;
using DevExpress.ExpressApp.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WindowAuthenticationService {
    public class WindowAuthenticationDataServiceHelper : ODataServiceHelper {
        public WindowAuthenticationDataServiceHelper(string connectionString, Assembly[] assemblies, string namespaceName) :
            base(connectionString, assemblies, namespaceName) {
        }
        public override IOperationPermissionProvider GetPermissionProvider(DevExpress.Xpo.UnitOfWork session) {
            IOperationPermissionProvider user = base.GetPermissionProvider(session);
            if(user == null) {
                DBUpdater updater = new DBUpdater();
                updater.AddNewUserContact(XPDictionary, session, CurrentUserName);
                user = base.GetPermissionProvider(session);
            }
            return user;
        }
    }
}