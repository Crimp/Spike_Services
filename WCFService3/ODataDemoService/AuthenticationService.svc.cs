using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.Web.Security;
using System.Web;
using TestAuthenticationService;

public class AuthenticationService : AuthenticationServiceBase {
    public AuthenticationService() {
    }
}
