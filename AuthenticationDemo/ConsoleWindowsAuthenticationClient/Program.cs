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
        public static string customAuthenticationRootDataUrl = "http://localhost:54002/CustomAuthenticationDataService.svc";
        static void Main(string[] args) {
            Console.WriteLine("UnSecured");
            DataServiceContext unSecuredDataContext = new DataServiceContext(new Uri(unSecuredRootDataUrl));
            ShowData(unSecuredDataContext);


            Console.WriteLine("WindowAuthentication");
            DataServiceContext windowAuthenticationDataContext = new DataServiceContext(new Uri(windowAuthenticationRootDataUrl));
            ShowData(windowAuthenticationDataContext);

            Console.WriteLine("CustomAuthentication for does not exist user 'NotExistUser'");
            DataServiceContext customAuthenticationDataContext = new DataServiceContext(new Uri(customAuthenticationRootDataUrl));
            customAuthenticationDataContext.SendingRequest += new EventHandler<SendingRequestEventArgs>(delegate(object sender, SendingRequestEventArgs e) {
                e.RequestHeaders.Add("UserName", "NotExistUser");
                e.RequestHeaders.Add("Password", "");
            });
            ShowData(customAuthenticationDataContext);

            Console.WriteLine("CustomAuthentication for user 'Sam'");
            customAuthenticationDataContext = new DataServiceContext(new Uri(customAuthenticationRootDataUrl));
            customAuthenticationDataContext.SendingRequest += new EventHandler<SendingRequestEventArgs>(delegate(object sender, SendingRequestEventArgs e) {
                e.RequestHeaders.Add("UserName", "Sam");
                e.RequestHeaders.Add("Password", "sam");
            });
            ShowData(customAuthenticationDataContext);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
        private static void ShowData(DataServiceContext dataContext) {
            try {
                
                var contactQuery = (from p in dataContext.CreateQuery<Contact>("Contact") select p);
                ConsoleWriter consoleWriter = new ConsoleWriter();
                consoleWriter.PrintData(contactQuery);

                //Contact test = contactQuery.First<Contact>();
                //test.FirstName = test.FirstName + "deddsdsdsd";

                //dataContext.UpdateObject(test);

                //dataContext.SaveChanges();

                //var contactQuery2 = (from p in dataContext.CreateQuery<Contact>("Contact") select p);
                //consoleWriter.PrintData(contactQuery2);
            }
            catch(Exception e) {
                Console.WriteLine("Error: " + e.InnerException.Message);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("-------------------------------------------");
            }
        }
    }
}
