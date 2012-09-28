using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Security;

[ServiceContract]
public interface IAuthenticationService {
    [OperationContract]
    string ValidateUser(string userName, string password);
}
