using BusinessObjectsLibrary;
using DataProvider;
using DevExpress.Data.Filtering;
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
        public void Update(string connectionString, Assembly assembly) {
            ODataServiceHelper hellper = new ODataServiceHelper(connectionString, assembly, "");
            CreateTestData(hellper.CreateSession(AutoCreateOption.DatabaseAndSchema), hellper);
        }
        private void CreateTestData(UnitOfWork session, ODataServiceHelper hellper) {

            Contact contactSam = session.FindObject<Contact>(CriteriaOperator.Parse("FirstName == 'Mary'"));
            XPClassInfo contactClassInfo = hellper.XPDictionary.GetClassInfo(typeof(Contact));
            if (contactSam == null)
            {
                contactSam = (Contact)contactClassInfo.CreateNewObject(session);
                contactSam.FirstName = "Mary";
                contactSam.LastName = "Tellitson";
                contactSam.Save();
            }
            session.CommitChanges();
        }
    }
}