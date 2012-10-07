using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace DataProvider {
    public class ODataServiceHelper {
        private static XPDictionary _xPDictionary = null;
        public ODataServiceHelper(string connectionString, Assembly assembly, string namespaceName) {
            this.ConnectionString = connectionString;
            this.Assembly = assembly;
            this.NamespaceName = namespaceName;
        }
        public IObjectLayer CreateDataLayer() {
            SessionObjectLayer sessionObjectLayer = new SessionObjectLayer(CreateSession(), true, null, new SecurityRuleProvider(XPDictionary, CreateSelectDataSecurity()), null);
            return sessionObjectLayer;
        }
        public UnitOfWork CreateSession(DevExpress.Xpo.DB.AutoCreateOption autoCreateOption) {
            IDataStore store = XpoDefault.GetConnectionProvider(ConnectionString, autoCreateOption);
            IDataLayer directDataLayer = new ThreadSafeDataLayer(XPDictionary, store);
            UnitOfWork directSession = new UnitOfWork(directDataLayer);
            return directSession;
        }
        public UnitOfWork CreateSession() {
            return CreateSession(DevExpress.Xpo.DB.AutoCreateOption.SchemaAlreadyExists);
        }

        public virtual Assembly Assembly {
            get;
            set;
        }
        public string ConnectionString {
            get;
            set;
        }
        public string NamespaceName {
            get;
            set;
        }

        protected virtual string CurrentUserName {
            get {
                return HttpContext.Current.User.Identity.Name;
            }
        }

        public XPDictionary XPDictionary {
            get {
                if(_xPDictionary == null) {
                    _xPDictionary = new ReflectionDictionary();
                    _xPDictionary.GetDataStoreSchema(Assembly);
                }
                return _xPDictionary;
            }
        }
        //private static XPDictionary GetXPDictionary(Assembly assembly) {
        //    if(_xPDictionary == null) {
        //        _xPDictionary = new ReflectionDictionary();
        //        _xPDictionary.GetDataStoreSchema(assembly);
        //    }
        //    return _xPDictionary;
        //}
        private ISelectDataSecurity CreateSelectDataSecurity() {
            UnitOfWork session = CreateSession();
            string userName = CurrentUserName;
            IOperationPermissionProvider user = session.FindObject(typeof(SecuritySystemUser), new BinaryOperator("UserName", userName)) as IOperationPermissionProvider;
            IEnumerable<IOperationPermission> userPermissions = null;
            if(user != null) {
                userPermissions = OperationPermissionProviderHelper.CollectPermissionsRecursive(user);
            } else {
                userPermissions = new List<IOperationPermission>();
            }
            IPermissionDictionary permissionsDictionary = new PermissionDictionary(userPermissions);

            IDictionary<Type, IPermissionRequestProcessor> processors = new Dictionary<Type, IPermissionRequestProcessor>();
            processors.Add(typeof(ModelOperationPermissionRequest), new ModelPermissionRequestProcessor(permissionsDictionary));
            processors.Add(typeof(ServerPermissionRequest), new ServerPermissionRequestProcessor(permissionsDictionary));
            return new SelectDataSecurity(processors, permissionsDictionary);
        }
    }
}
