using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace DXTremeDemoService {
    [ServiceContract(Namespace = "")]
    //[JSONPSupportBehavior]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AuthenticationService {
        [OperationContract]
        public string ValidateUser(string userName, string password) {
            return userName + password;
        }
    }
}
