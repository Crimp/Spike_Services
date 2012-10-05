using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WindowsAuthenticationConsoleClient.ODataDemoService;

namespace WindowsAuthenticationConsoleClient {
    class Program {
        public static string rootDataUrl = "http://localhost:63725/ODataDemoService/ODataDemoService.svc";
        static void Main(string[] args) {
            DataServiceContext dataContext = new DataServiceContext(new Uri(rootDataUrl));
            dataContext.Credentials = CredentialCache.DefaultCredentials;
            try {
                ShowData(dataContext);
            }
            catch {
                Console.WriteLine("Error");
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
    }
}
