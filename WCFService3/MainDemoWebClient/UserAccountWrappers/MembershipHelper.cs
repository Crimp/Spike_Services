using System.Web;
using System.Web.Security;
using System.Security.Principal;
using System.Threading;
namespace MainDemoWebClient.UserAccountWrappers {
    public static class MembershipHelper {
        public static string UserName {
            get {
                return System.Threading.Thread.CurrentPrincipal.Identity.Name;
            }
        }
        public static bool IsUserLoggedIn() {
            return System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated;
        }
        public static bool IsWindowsAuthentication {
            get {
                return Thread.CurrentPrincipal.Identity is WindowsIdentity;
            }
        }
    }
}