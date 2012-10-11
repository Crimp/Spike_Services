using BusinessObjectsLibrary;
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
using System.Security.Principal;
using System.Web;

namespace DBCreator {
    public class DBUpdater {
        string connectionString;
        XPDictionary xpDictionary;
        UnitOfWork session;
        public DBUpdater(string connectionString, Assembly[] assemblies) {
            this.connectionString = connectionString;
            xpDictionary = new ReflectionDictionary();
            List<Assembly> _assemblies = new List<Assembly>(assemblies);
            _assemblies.Add(typeof(SecuritySystemUser).Assembly);
            xpDictionary.GetDataStoreSchema(_assemblies.ToArray<Assembly>());

            session = CreateUpdatingSession();
        }
        public void CreateDB() {
            CreateTestData();
        }
        public void AddNewUser(string userName, string lastName, string email, string password) {
            XPClassInfo securitySystemUserInfo = xpDictionary.GetClassInfo(typeof(SecuritySystemUser));
            string criteria = string.Format("UserName == '{0}'", userName);
            SecuritySystemUser newUser = session.FindObject<SecuritySystemUser>(CriteriaOperator.Parse(criteria));
            if(newUser == null) {
                newUser = (SecuritySystemUser)securitySystemUserInfo.CreateNewObject(session);
                newUser.UserName = userName;
                newUser.SetPassword(password);

                SecuritySystemRole userRole = session.FindObject<SecuritySystemRole>(new BinaryOperator("Name", "Users"));
                newUser.Roles.Add(userRole);
                newUser.Save();
                session.CommitChanges();
            }

            XPClassInfo contactClassInfo = xpDictionary.GetClassInfo(typeof(Contact));
            criteria = string.Format("FirstName == '{0}' && LastName == '{1}'", userName, lastName);
            Contact newUserContact = session.FindObject<Contact>(CriteriaOperator.Parse(criteria));
            if(newUserContact == null) {
                newUserContact = (Contact)contactClassInfo.CreateNewObject(session);
                newUserContact.FirstName = userName;
                newUserContact.LastName = lastName;
                newUserContact.Email = email;
                newUserContact.OwnerId = newUser.Oid;
                session.CommitChanges();
            }
        }
        public UnitOfWork CreateUpdatingSession() {
            IDataStore store = XpoDefault.GetConnectionProvider(connectionString, AutoCreateOption.DatabaseAndSchema);
            IDataLayer directDataLayer = new ThreadSafeDataLayer(xpDictionary, store);
            UnitOfWork directSession = new UnitOfWork(directDataLayer);
            return directSession;
        }
        private void CreateTestData() {
            CreateAdminRole();
            CreateUserRole();
            session.CommitChanges();

            AddNewUser("Mary", "Tellitson", "mary_tellitson@md.com", "mary");
            AddNewUser("Sam", "Tellitson", "sam_tellitson@md.com", "sam");
            AddNewUser("John", "Nilsen", "john_nilsen@md.com", "john");
            AddNewUser(WindowsIdentity.GetCurrent().Name, "", "", "");

            session.CommitChanges();
        }
        private void CreateAdminRole() {
            XPClassInfo securitySystemUserInfo = xpDictionary.GetClassInfo(typeof(SecuritySystemUser));
            SecuritySystemUser userAdmin = session.FindObject<SecuritySystemUser>(new BinaryOperator("UserName", "Admin"));
            if(userAdmin == null) {
                userAdmin = (SecuritySystemUser)securitySystemUserInfo.CreateNewObject(session);
                userAdmin.UserName = "Admin";
                userAdmin.SetPassword("Admin");

                SecuritySystemRole adminRole = session.FindObject<SecuritySystemRole>(new BinaryOperator("Name", "Administrators"));
                XPClassInfo securitySystemRoleInfo = xpDictionary.GetClassInfo(typeof(SecuritySystemRole));
                if(adminRole == null) {
                    adminRole = (SecuritySystemRole)securitySystemRoleInfo.CreateNewObject(session);
                    adminRole.Name = "Administrators";
                }
                adminRole.IsAdministrative = true;
                userAdmin.Roles.Add(adminRole);
            }
        }
        private void CreateUserRole() {
            SecuritySystemRole userRole = session.FindObject<SecuritySystemRole>(new BinaryOperator("Name", "Users"));
            if(userRole == null) {
                XPClassInfo securitySystemRoleInfo = xpDictionary.GetClassInfo(typeof(SecuritySystemRole));
                userRole = (SecuritySystemRole)securitySystemRoleInfo.CreateNewObject(session);
                userRole.Name = "Users";

                XPClassInfo typePermissionsObjectInfo = xpDictionary.GetClassInfo(typeof(SecuritySystemTypePermissionObject));

                SecuritySystemTypePermissionObject typePermissions = (SecuritySystemTypePermissionObject)typePermissionsObjectInfo.CreateNewObject(session);
                typePermissions.TargetType = typeof(Contact);
                typePermissions.AllowNavigate = true;
                typePermissions.Save();
                userRole.TypePermissions.Add(typePermissions);


                XPClassInfo permissionsObjectInfo = xpDictionary.GetClassInfo(typeof(SecuritySystemObjectPermissionsObject));
                SecuritySystemObjectPermissionsObject fullAccessObjectPermission = (SecuritySystemObjectPermissionsObject)permissionsObjectInfo.CreateNewObject(session);
                fullAccessObjectPermission.Criteria = "[OwnerId] = CurrentUserId()";
                //fullAccessObjectPermission.AllowDelete = true;
                fullAccessObjectPermission.AllowNavigate = true;
                fullAccessObjectPermission.AllowRead = true;
                fullAccessObjectPermission.AllowWrite = true;
                fullAccessObjectPermission.Save();
                typePermissions.ObjectPermissions.Add(fullAccessObjectPermission);
            }
        }
    }
}