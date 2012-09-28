
namespace ODataDemoService {
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Security;
    using MainDemoWebClient.Models;
    using MainDemoWebClient.UserAccountWrappers;
    using System.Data.Services.Client;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using System.Net.Security;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.ClientServices;
    using System.Threading;
    using System.Web;
    using MainDemoWebClient.ODataDemoService;

    public class ContactsController : Controller {
        public static string rootDataUrl = "http://minakov-w8.corp.devexpress.com/ODataDemoService/ODataDemoService.svc";
        public ContactsController() {

        }
        public ActionResult Index() {
            DataServiceContext context = new DataServiceContext(new Uri(rootDataUrl));
            context.SendingRequest += new EventHandler<SendingRequestEventArgs>(context_SendingRequest);
            var contactQuery = (from p in context.CreateQuery<Module_BusinessObjects_Contact>("Module_BusinessObjects_Contact").Expand("Department").Expand("Position")
                                select p);
            return this.View(contactQuery);
        }

        void context_SendingRequest(object sender, SendingRequestEventArgs e) {
            if(Thread.CurrentPrincipal.Identity.IsAuthenticated) {
                FormsAuthenticationTicket ticket = ((FormsIdentity)Thread.CurrentPrincipal.Identity).Ticket;
                //string version = e.RequestHeaders["MaxDataServiceVersion"];
                //e.RequestHeaders["MaxDataServiceVersion"] = version.Replace("2", "3");
                e.RequestHeaders.Add("Authorization", FormsAuthentication.Encrypt(ticket));
            }
        }
        public ActionResult Detail(Guid id) {
            //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            DataServiceContext context = new DataServiceContext(new Uri(rootDataUrl));
            context.SendingRequest += new EventHandler<SendingRequestEventArgs>(context_SendingRequest);
            DataServiceQuery<Module_BusinessObjects_Contact> personsQuery = context.CreateQuery<Module_BusinessObjects_Contact>("Module_BusinessObjects_Contact").Expand("Department").Expand("Position");
            var collection = personsQuery.Where<Module_BusinessObjects_Contact>(x => x.oid == id).ToList<Module_BusinessObjects_Contact>();
            var result = collection.Count > 0 ? collection[0] : null;
            return this.View(result);
        }
    }
}
