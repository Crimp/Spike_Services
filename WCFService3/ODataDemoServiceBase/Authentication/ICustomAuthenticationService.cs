using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Security;

[ServiceContract]
public interface ICustomAuthenticationService {
    //[WebGet(ResponseFormat = WebMessageFormat.Json)]
    [OperationContract]
    string ValidateUser(string userName, string password);
}
