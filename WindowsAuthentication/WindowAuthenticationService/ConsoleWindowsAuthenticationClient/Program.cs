using ConsoleWindowsAuthenticationClient.WindowAuthenticationDataService;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Net;
using System.Text;

namespace ConsoleWindowsAuthenticationClient {
    class Program {
        public static string windowAuthenticationRootDataUrl = "http://localhost:50731/WindowAuthenticationDataService.svc";
        public static string unSecuredRootDataUrl = "http://localhost:62445/UnSecuredDataService.svc";
        static void Main(string[] args) {
            DataServiceContext unSecuredDataContext = new DataServiceContext(new Uri(unSecuredRootDataUrl));
            Console.WriteLine("UnSecured");
            ShowData(unSecuredDataContext);

            DataServiceContext windowAuthenticationDataContext = new DataServiceContext(new Uri(windowAuthenticationRootDataUrl));
            windowAuthenticationDataContext.Credentials = CredentialCache.DefaultCredentials;
            Console.WriteLine("WindowAuthentication");
            ShowData(windowAuthenticationDataContext);
            Console.ReadKey();
        }
        private static void ShowData(DataServiceContext dataContext) {
            try {
                
                var contactQuery = (from p in dataContext.CreateQuery<Contact>("Contact") select p);
                ConsoleWriter consoleWriter = new ConsoleWriter();
                consoleWriter.PrintData(contactQuery);
            }
            catch {
                Console.WriteLine("Error");
            }
        }
    }
}
