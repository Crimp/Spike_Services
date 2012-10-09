using BusinessObjectsLibrary;
using DataProvider;
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
using System.Web;

namespace WindowAuthenticationService {
    public class DBUpdater {
        public void Update(string connectionString, Assembly[] assemblies) {
            ODataServiceHelper hellper = new ODataServiceHelper(connectionString, assemblies, "");
            CreateTestData(hellper.CreateSession(AutoCreateOption.DatabaseAndSchema), hellper);
        }
        public void AddNewUserContact(XPDictionary xpDictionary, UnitOfWork session, string userName) {
            XPClassInfo contactClassInfo = xpDictionary.GetClassInfo(typeof(Contact));

            Contact newUserContact = (Contact)contactClassInfo.CreateNewObject(session);
            newUserContact.FirstName = userName;

            XPClassInfo securitySystemUserInfo = xpDictionary.GetClassInfo(typeof(SecuritySystemUser));
            SecuritySystemUser newUser = (SecuritySystemUser)securitySystemUserInfo.CreateNewObject(session);
            newUser = (SecuritySystemUser)securitySystemUserInfo.CreateNewObject(session);
            newUser.UserName = userName;
            newUser.SetPassword("");

            SecuritySystemRole userRole = session.FindObject<SecuritySystemRole>(new BinaryOperator("Name", "Users"));

            newUser.Roles.Add(userRole);
            session.CommitChanges();            
        }
        private void CreateTestData(UnitOfWork session, ODataServiceHelper hellper) {
            XPClassInfo contactClassInfo = hellper.XPDictionary.GetClassInfo(typeof(Contact));

            Contact contactMary = session.FindObject<Contact>(CriteriaOperator.Parse("FirstName == 'Mary' && LastName == 'Tellitson'"));
            if(contactMary == null) {
                contactMary = (Contact)contactClassInfo.CreateNewObject(session);
                contactMary.FirstName = "Mary";
                contactMary.LastName = "Tellitson";
                contactMary.Email = "mary_tellitson@md.com";
            }
            Contact contactSam = session.FindObject<Contact>(CriteriaOperator.Parse("FirstName == 'Sam' && LastName == 'Tellitson'"));
            if(contactMary == null) {
                contactSam = (Contact)contactClassInfo.CreateNewObject(session);
                contactSam.FirstName = "Sam";
                contactSam.LastName = "Tellitson";
                contactSam.Email = "sam_tellitson@md.com";
            }
            Contact contactJohn = session.FindObject<Contact>(CriteriaOperator.Parse("FirstName == 'John' && LastName == 'Nilsen'"));
            if(contactJohn == null) {
                contactJohn = (Contact)contactClassInfo.CreateNewObject(session);
                contactJohn.FirstName = "John";
                contactJohn.LastName = "Nilsen";
                contactJohn.Email = "john_nilsen@md.com";
            }
            session.CommitChanges();

            //Create Users for the Complex Security Strategy
            XPClassInfo securitySystemUserInfo = hellper.XPDictionary.GetClassInfo(typeof(SecuritySystemUser));
            SecuritySystemUser userAdmin = session.FindObject<SecuritySystemUser>(new BinaryOperator("UserName", "Admin"));
            if(userAdmin == null) {
                userAdmin = (SecuritySystemUser)securitySystemUserInfo.CreateNewObject(session);
                userAdmin.UserName = "Admin";
                userAdmin.SetPassword("Admin");
            }
            SecuritySystemRole adminRole = session.FindObject<SecuritySystemRole>(new BinaryOperator("Name", "Administrators"));
            XPClassInfo securitySystemRoleInfo = hellper.XPDictionary.GetClassInfo(typeof(SecuritySystemRole));
            if(adminRole == null) {
                adminRole = (SecuritySystemRole)securitySystemRoleInfo.CreateNewObject(session);
                adminRole.Name = "Administrators";
            }
            adminRole.IsAdministrative = true;

            //SecuritySystemRole userRole = session.FindObject<SecuritySystemRole>(new BinaryOperator("Name", "Users"));
            //if(userRole == null) {
            //    userRole = (SecuritySystemRole)securitySystemRoleInfo.CreateNewObject(session);
            //    userRole.Name = "Users";
            //}
            //userRole.SetTypePermissionsRecursively<Contact>(SecurityOperations.FullAccess, SecuritySystemModifier.Deny);
            //userRole.SetTypePermissionsRecursively<SecuritySystemUser>(SecurityOperations.FullAccess, SecuritySystemModifier.Deny);
            //userRole.SetTypePermissionsRecursively<SecuritySystemRole>(SecurityOperations.FullAccess, SecuritySystemModifier.Deny);

            userAdmin.Roles.Add(adminRole);

            CreateUserRole(session, hellper.XPDictionary);

            session.CommitChanges();
        }
        private SecuritySystemRole CreateUserRole(UnitOfWork session, XPDictionary xpDictionary) {
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
                fullAccessObjectPermission.Criteria = "[FirstName] = CurrentUserId()";
                fullAccessObjectPermission.AllowDelete = true;
                fullAccessObjectPermission.AllowNavigate = true;
                fullAccessObjectPermission.AllowRead = true;
                fullAccessObjectPermission.AllowWrite = true;
                fullAccessObjectPermission.Save();
                typePermissions.ObjectPermissions.Add(fullAccessObjectPermission);
            }
            userRole.SetTypePermissionsRecursively<Contact>(SecurityOperations.FullAccess, SecuritySystemModifier.Deny);
            userRole.SetTypePermissionsRecursively<SecuritySystemUser>(SecurityOperations.FullAccess, SecuritySystemModifier.Deny);
            userRole.SetTypePermissionsRecursively<SecuritySystemRole>(SecurityOperations.FullAccess, SecuritySystemModifier.Deny);

            return userRole;
        }
    }
}