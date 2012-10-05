using FormsAuthenticationConsoleClient.AuthenticationService;
using FormsAuthenticationConsoleClient.ODataDemoService;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FormsAuthenticationConsoleClient {
    class Program {
        //public static string rootDataUrl = "https://severname/ODataDemoService/ODataDemoService.svc";
        public static string rootDataUrl = "https://minakov-w8.corp.devexpress.com/ODataDemoService/ODataDemoService.svc";
        static void Main(string[] args) {
            Console.WriteLine("FormsAuthentication");
            Console.Write("UserName:");
            string userName = Console.ReadLine();
            Console.Write("Password:");
            string password = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Authorization");
            string authenticationTicket = GetAuthenticationTicket(userName, password);
            if(!string.IsNullOrEmpty(authenticationTicket)) {
                Console.WriteLine("Success");
                DataServiceContext dataContext = new DataServiceContext(new Uri(rootDataUrl));
                dataContext.Credentials = System.Net.CredentialCache.DefaultCredentials;
                dataContext.SendingRequest += new EventHandler<SendingRequestEventArgs>(delegate(object sender, SendingRequestEventArgs e) {
                    e.RequestHeaders.Add("Authorization", authenticationTicket);
                });
                ShowData(dataContext);
            }
            else {
                Console.WriteLine("The user name or password provided is incorrect.");
            }
            Console.ReadLine();
        }
        private static void ShowData(DataServiceContext dataContext) {
            var contactQuery = (from p in dataContext.CreateQuery<Module_BusinessObjects_Contact>("Module_BusinessObjects_Contact") select p);
            Console.WriteLine("Begin request");
            foreach(Module_BusinessObjects_Contact contact in contactQuery) {
                Console.WriteLine("FirstName:" + contact.FirstName);
            }
            Console.WriteLine("End request");
        }
        private static string GetAuthenticationTicket(string userName, string password) {
            AuthenticationServiceClient authenticationService = new AuthenticationServiceClient();
            return authenticationService.ValidateUser(userName, password);
        }
    }
}
